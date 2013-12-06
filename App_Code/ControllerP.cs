using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

using Ding.Core;
using Ding.Enums;
using Ding.Interfaces;
using Ding.Structures;

namespace Elim.Core
{
	public class ControllerP<T> where T : class, IModel, new()
	{
		public ControllerP()
		{
			ModelIsNew = false;
			DB = Database.New();
			Server = HttpContext.Current.Server;

			HttpRequest Request = HttpContext.Current.Request;
			string PageName = (new FileInfo(Request.Url.AbsolutePath)).Name;
			ControllerInfo = new ControllerInfo();
			ControllerInfo.Section = PageName.Replace(".aspx", "");
			ControllerInfo.ModelType = typeof(T).Name;
			ControllerInfo.PageName = PageName;

			SetModel();
		}

		#region Model Basics
		public T SetModel(Guid ModelId)
		{
			SetModel(ModelId.NullG());
			return Model;
		}

		public void SetModel()
		{
			SetModel(null);
		}

		public void SetModel(Guid? ModelId)
		{
			M = new M<T>();

			M.Model = ModelId ?? Guid.Empty;
			if (M.Model.IsEmpty())
			{
				M.Instance = new T();
				Model = M.Instance;
				M.Model = Guid.NewGuid();
				ModelIsNew = true;
				return;
			}

			bool ModelValidation = DB.GetValue("ModelValidation", "@Model", M.Model).B();
			if (!ModelValidation)
			{
				M.Instance = new T();
				Model = M.Instance;
				return;
			}

			byte[] Instance = (byte[])DB.GetValue("ModelInstance", "@Model", M.Model);
			object InstanceObject = General.Deserialize(Instance);

			M.Instance = InstanceObject as T;

			if (M.Instance == null)
			{
				M.Instance = new T();
				Model = M.Instance;
				M.Model = Guid.NewGuid();
				ModelIsNew = true;
				return;
			}

			Model = M.Instance;
		}

		/// <summary>
		/// Save the Model using the Model guid into the database as a serialized object.
		/// </summary>
		[DebuggerStepThrough]
		public void SaveModel()
		{
			DB.Save("Model", "@Model", M.Model, "@Instance", General.Serialize(Model));
		}

		/// <summary>
		/// Delete the Model using the Model guid
		/// </summary>
		[DebuggerStepThrough]
		public void DeleteModel()
		{
			DB.Delete("Model", "@Model", M.Model);
		}
		#endregion

		#region GetMessages
		public string GetMessagesFormatPlane()
		{
			string message = "";
			foreach (Message Message in Model.Messages)
				message += Message.Variable + "|" + Message.Name + "|" + Message.MessageType.S() + ",";

			if (message.Length > 1)
				message = message.Substring(0, message.Length - 1);
			return message;
		}
		public string GetMessagesFormatJson()
		{
			string messages = "";
			foreach (Message Message in Model.Messages)
				messages += "{\"Variable\":\"" + Message.Variable + "\", \"Name\":\"" + Message.Name + "\", \"MessageType\":\"" + Message.MessageType.S() + "\"},";

			if (messages.Length > 1)
				messages = messages.Substring(0, messages.Length - 1);

			bool ValidationMessages = Model.Messages.Exists(x => x.MessageType == MessageType.Validation);
			bool SuccessMessages = Model.Messages.Exists(x => x.MessageType == MessageType.Success);
			bool ErrorMessages = Model.Messages.Exists(x => x.MessageType == MessageType.Error);
			bool InformationMessages = Model.Messages.Exists(x => x.MessageType == MessageType.Information);
			bool WarningMessages = Model.Messages.Exists(x => x.MessageType == MessageType.Warning);

			StringBuilder JsonString = new StringBuilder();

			JsonString.AppendParameterizedFormat("{\"TranslateText\" : \"{TranslateText}\", \"Messages\" : [{Messages}], \"MessageTypes\" :{\"ValidationMessages\":\"{ValidationMessages}\",\"SuccessMessages\":\"{SuccessMessages}\",\"ErrorMessages\":\"{ErrorMessages}\",\"InformationMessages\":\"{InformationMessages}\",\"WarningMessages\":\"{WarningMessages}\"}}", "{TranslateText}", GetMessages("General").Replace("\"", "'"), "{Messages}", messages, "{ValidationMessages}", ValidationMessages, "{SuccessMessages}", SuccessMessages, "{ErrorMessages}", ErrorMessages, "{InformationMessages}", InformationMessages, "{WarningMessages}", WarningMessages);
			return JsonString.ToString();
		}
		public string GetMessagesType()
		{
			List<string> messgeTypes = new List<string>();

			foreach (Message Message in Model.Messages)
				if (!messgeTypes.Contains(Message.MessageType.S()))
					messgeTypes.Add(Message.MessageType.S());

			return string.Join(",", messgeTypes.ToArray());
		}

		public string GetMessages()
		{
			return GetMessages(Model);
		}
		public string GetMessages(IModel model)
		{
			return GetMessages("General", model);
		}
		public string GetMessages(string group, IModel model)
		{
			if (model == null) return string.Empty;
			if (model.Messages.IsNull()) return string.Empty;

			StringBuilder HtmlMessages = new StringBuilder();
			foreach (string MessageTypeName in Enum.GetNames(typeof(MessageType)))
			{
				MessageType MessageType = (MessageType)Enum.Parse(typeof(MessageType), MessageTypeName);
				StringBuilder HtmlMessagesByMessageType = new StringBuilder();

				Collection<Message> MessagesListByMessageType = model.Messages.FindAll(Message => Message.Group.Equals(group) && Message.MessageType == MessageType && Message.ForModel.IsEmpty());
				HtmlMessagesByMessageType.Append("<ul>");
				foreach (Message Message in MessagesListByMessageType)
				{
					HtmlMessagesByMessageType.AppendFormat("<li>{0}</li>", GetMessageText(Message.Name, Message.MessageCollection));
				}
				HtmlMessagesByMessageType.Append("</ul>");

				Collection<Message> MessagesListByMessageTypeAndByForModel = model.Messages.FindAll(Message => Message.Group.Equals(group) && Message.MessageType == MessageType && Message.ForModel.IsFilled());
				List<string> ForModelList = MessagesListByMessageTypeAndByForModel.ConvertAll(Message => Message.ForModel).Distinct().ToList();

				foreach (string ForModel in ForModelList)
				{
					Collection<Message> MessagesByForModel = MessagesListByMessageTypeAndByForModel.FindAll(Message => Message.ForModel == ForModel);
					StringBuilder MultiVariables = new StringBuilder();
					foreach (Message MessageByForModel in MessagesByForModel)
					{
						MultiVariables.AppendFormat("{0}, ", MessageByForModel.MessageCollection[0].TranslatedValue);
					}
					MultiVariables = MultiVariables.Trim(new char[] { ',', ' ' });
					HtmlMessagesByMessageType.AppendFormat("<ul><li>{0}</li></ul>", GetMessageText("RowFieldMissing", new MessageCollection() { { "MultiVariables", MultiVariables } }));
				}
				int TotalMessages = MessagesListByMessageType.Count + MessagesListByMessageTypeAndByForModel.Count;
				if (TotalMessages == 1)
					if (MessagesListByMessageType.Count == 1)
						HtmlMessages.Append(GetMessage(MessagesListByMessageType[0]));
					else
						HtmlMessages.Append(GetMessage(MessagesListByMessageTypeAndByForModel[0]));
				else if (TotalMessages > 0)
					HtmlMessages.AppendFormat(MessageFormat, MessageType, group + MessageTypeName, HtmlMessagesByMessageType);
			}
			return HtmlMessages.ToString();
		}
		public string GetMessages(string group)
		{
			return GetMessages(group, Model);
		}
		public string GetMessage(Message message)
		{
			if (message == null) throw new ArgumentNullException("message");

			return GetMessage(message.Name, message.MessageType, message.MessageCollection);
		}
		public string GetMessage(string name, MessageType type, MessageCollection messageCollection)
		{
			return MessageFormat.SFormat(type, name, GetMessageText(name, messageCollection));
		}
		public static string GetMessageText(string name, MessageCollection messageCollection)
		{
			string MessageText = Dictionary.S(name);
			foreach (MessageParameter MessageParam in messageCollection)
			{
				MessageText = MessageText.Replace("{" + MessageParam.Key + "}", MessageParam.TranslatedValue);
			}
			return MessageText;
		}
		private string MessageFormat = "<div class='Message {0}' id='{1}'>{2}</div>";
		#endregion

		#region ClearMessages
		public void ClearMessages()
		{
			ClearMessages(Model);
		}
		public void ClearMessages(MessageType messageType)
		{
			ClearMessages(Model, messageType);
		}
		public void ClearMessages(IModel model)
		{
			if (model == null) throw new ModelIsNullException();
			model.Messages.Clear();
		}
		public void ClearMessages(IModel model, MessageType messageType)
		{
			if (model == null) throw new ModelIsNullException();
			model.Messages.RemoveAll(x => x.MessageType == messageType);
		}
		#endregion

		#region AddMessages
		public void AddMessage(string name, MessageType type)
		{
			AddMessage("", name, type, new MessageCollection(), Model);
		}
    public void AddMessage(string name, MessageType type, MessageCollection messageCollection)
    {
      AddMessage("", name, type, messageCollection, Model);
    }
		public void AddMessage(string variable, string name, MessageType type, IModel model)
		{
			AddMessage(variable, name, type, new MessageCollection(), model);
		}
		public void AddMessage(string variable, string name, MessageType type, MessageCollection messageCollection, IModel model)
		{
			AddMessage(variable, "General", name, type, messageCollection, model);
		}
		public void AddMessage(string variable, string group, string name, MessageType type, IModel model)
		{
			AddMessage(variable, group, name, type, new MessageCollection(), model);
		}
		public void AddMessage(string variable, string group, string name, MessageType type, MessageCollection messageCollection, IModel model)
		{
			if (model == null) throw new ModelIsNullException();
			if (model.Messages.IsNull()) ClearMessages(model);

			if (model.Messages.Exists(x => x.Name == name && x.Group == group && x.MessageType == type && x.MessageCollection == messageCollection)) return;

			string ForModel = "";
			if (messageCollection != null)
			{
				foreach (MessageParameter MessageParam in messageCollection)
				{
					if (MessageParam.Key == "Variable")
					{
						int IndexOfFor = MessageParam.Value.IndexOf("For", StringComparison.Ordinal);
						if (IndexOfFor != -1)
						{
							string Value = MessageParam.Value.Substring(0, IndexOfFor);
							ForModel = MessageParam.Value.Substring(IndexOfFor + 3);
							MessageParam.Value = Value;
						}
					}
				}
			}
			model.Messages.Add(new Message(variable, name, group, ForModel, type, messageCollection));
		}
		public void AddMessage(string variable, string name, MessageType type)
		{
			AddMessage(variable, name, type, new MessageCollection(), Model);
		}
		public void AddMessage(string variable, string name, MessageType type, MessageCollection messageCollection)
		{
			AddMessage(variable, name, type, messageCollection, Model);
		}
		public void AddMessage(string variable, string group, string name, MessageType type, MessageCollection messageCollection)
		{
			AddMessage(variable, group, name, type, messageCollection, Model);
		}
		public void AddMessage(string variable, string group, string name, MessageType type)
		{
			AddMessage(variable, group, name, type, new MessageCollection());
		}
		#endregion

		#region HasMessage
		public bool HasMessage(string name, IModel model)
		{
			if (model == null) throw new ModelIsNullException();
			if (model.Messages == null) return false;

			foreach (Message Message in model.Messages)
			{
				if (Message.Name == name) return true;
				if (Message.MessageCollection.Exists(x => x.Key == name)) return true;
			}
			return false;
		}
		public bool HasMessage(string name)
		{
			return HasMessage(name, Model);
		}
		#endregion

		#region ContainMessages
		public bool ContainMessages
		{
			get
			{
				return ModelContainMessages(Model);
			}
		}
		public bool ContainErrorMessages
		{
			get
			{
				return ModelContainMessageType(Model, MessageType.Error);
			}
		}
		public bool ContainValidationMessages
		{
			get
			{
				return ModelContainMessageType(Model, MessageType.Validation);
			}
		}
		public bool ContainWarningMessages
		{
			get
			{
				return ModelContainMessageType(Model, MessageType.Warning);
			}
		}
		public bool ContainAlerts
		{
			get
			{
				return ContainErrorMessages || ContainValidationMessages || ContainWarningMessages;
			}
		}

		private bool ModelContainMessages(IModel model)
		{
			if (model == null) throw new ModelIsNullException();
			if (model.Messages == null) return false;
			return model.Messages.Count > 0;
		}
		private bool ModelContainMessageType(IModel model, MessageType messageType)
		{
			if (model == null) throw new ModelIsNullException();
			if (model.Messages == null) return false;
			return model.Messages.Count(x => x.MessageType == messageType) > 0;
		}
		#endregion

		#region Validators
		#region Validate NullDate
		public DateTime? ValidateNullDate(string variable, string value, string group, IModel model)
		{
			string VariableDate = value == null ? null : value.Trim();
			if (VariableDate.IsEmpty())
				AddMessage(variable, group, "EmptyInput", MessageType.Validation, new MessageCollection() { { "Variable", variable, true } }, model);

			else if (!VariableDate.IsDate())
				AddMessage(variable, group, "BadFormat", MessageType.Validation, new MessageCollection() { { "Variable", variable, true } }, model);

			else return value.NullDT();

			return new DateTime?();
		}

		public DateTime? ValidateNullDate(string variable, string value, IModel model)
		{
			return ValidateNullDate(variable, value, "General", model);
		}

		public DateTime? ValidateNullDate(string variable, string value)
		{
			return ValidateNullDate(variable, value, "General", Model);
		}

		public DateTime? ValidateNullDate(string variable, string value, string group)
		{
			return ValidateNullDate(variable, value, group, Model);
		}
		#endregion

		#region Validate TextArea
		public string ValidateTextArea(string variable, string value, string group, IModel model)
		{
			string VariableText = value == null ? null : Server.HtmlEncode(value.Trim());
			if (VariableText.IsEmpty())
				AddMessage(variable, group, "EmptyInput", MessageType.Validation, new MessageCollection() { { "Variable", variable, true } }, model);

			if (VariableText.IsFilled() && !VariableText.IsLegalTextArea())
				AddMessage(variable, group, "BadFormat", MessageType.Validation, new MessageCollection() { { "Variable", variable, true } }, model);

			return VariableText;
		}
		public string ValidateTextArea(string variable, string value, IModel model)
		{
			return ValidateTextArea(variable, value, "General", model);
		}
		public string ValidateTextArea(string variable, string value)
		{
			return ValidateTextArea(variable, value, "General", Model);
		}
		public string ValidateTextArea(string variable, string value, string group)
		{
			return ValidateTextArea(variable, value, group, Model);
		}
		#endregion

		#region Validate string
		public string ValidateString(string variable, string value, string group, IModel model)
		{
			string VariableString = value == null ? null : value.Trim();
			if (VariableString.IsEmpty())
				AddMessage(variable, group, "EmptyInput", MessageType.Validation, new MessageCollection() { { "Variable", variable, true } }, model);

			if (VariableString.IsFilled() && !VariableString.IsLegalInput())
				AddMessage(variable, group, "BadFormat", MessageType.Validation, new MessageCollection() { { "Variable", variable, true } }, model);

			return VariableString;
		}
		public string ValidateString(string variable, string value, IModel model)
		{
			return ValidateString(variable, value, "General", model);
		}
		public string ValidateString(string variable, string value)
		{
			return ValidateString(variable, value, "General", Model);
		}
		public string ValidateString(string variable, string value, string group)
		{
			return ValidateString(variable, value, group, Model);
		}
		#endregion

		#region Validata string with regex
		public string ValidateStringWithRegex(string variable, string value, string group, Regex regex, IModel model)
		{
			if (regex == null) throw new ArgumentNullException("regex");
			if (value.IsEmpty())
				AddMessage(variable, group, "EmptyInput", MessageType.Validation, new MessageCollection() { { "Variable", variable, true } }, model);

			if (value.IsFilled() && !regex.IsMatch(value))
				AddMessage(variable, group, "BadFormat", MessageType.Validation, new MessageCollection() { { "Variable", variable, true } }, model);

			return value;
		}
		public string ValidateStringWithRegex(string variable, string value, string pattern, IModel model)
		{
			return ValidateStringWithRegex(variable, value, "General", new Regex(pattern), model);
		}
		public string ValidateStringWithRegex(string variable, string value, Regex regex, IModel model)
		{
			return ValidateStringWithRegex(variable, value, "General", regex, model);
		}
		public string ValidateStringWithRegex(string variable, string value, string pattern)
		{
			return ValidateStringWithRegex(variable, value, "General", new Regex(pattern), Model);
		}
		public string ValidateStringWithRegex(string variable, string value, Regex regex)
		{
			return ValidateStringWithRegex(variable, value, "General", regex, Model);
		}
		public string ValidateStringWithRegex(string variable, string value, string group, Regex regex)
		{
			return ValidateStringWithRegex(variable, value, group, regex, Model);
		}
		#endregion

		#region Validate Null Decimal
		public decimal? ValidateNullDecimal(string variable, string value, string group, IModel model)
		{
			string VariableDecimal = value == null ? null : value.Trim();
			if (VariableDecimal.IsEmpty())
				AddMessage(variable, group, "EmptyInput", MessageType.Validation, new MessageCollection() { { "Variable", variable, true } }, model);

			else if (!VariableDecimal.IsDecimal())
				AddMessage(variable, group, "BadFormat", MessageType.Validation, new MessageCollection() { { "Variable", variable, true } }, model);

			else return value.NullD();
			return new decimal?();
		}
		public decimal? ValidateNullDecimal(string variable, string value, IModel model)
		{
			return ValidateNullDecimal(variable, value, "General", model);
		}
		public decimal? ValidateNullDecimal(string variable, string value)
		{
			return ValidateNullDecimal(variable, value, "General", Model);
		}
		public decimal? ValidateNullDecimal(string variable, string value, string group)
		{
			return ValidateNullDecimal(variable, value, group, Model);
		}
		#endregion

		#region Validate list
		public TList ValidateList<TList>(string variable, string value, string group, IModel model)
		{
			if (value.IsEmpty())
				AddMessage(variable, group, "EmptySelection", MessageType.Validation, new MessageCollection() { { "Variable", variable, true } }, model);

			if (typeof(TList) == typeof(Guid)) return (TList)Convert.ChangeType(value, typeof(TList), CultureInfo.InvariantCulture);
			if (typeof(TList) == typeof(String)) return (TList)Convert.ChangeType(value, typeof(TList), CultureInfo.InvariantCulture);

			return (TList)Convert.ChangeType(value, typeof(TList), CultureInfo.InvariantCulture);
		}
		public TList ValidateList<TList>(string variable, string value, IModel model)
		{
			return ValidateList<TList>(variable, value, "General", model);
		}
		public TList ValidateList<TList>(string variable, string value)
		{
			return ValidateList<TList>(variable, value, "General", Model);
		}
		public TList ValidateList<TList>(string variable, string value, string group)
		{
			return ValidateList<TList>(variable, value, group, Model);
		}
		#endregion

		#region Validate checkboxes
		public Collection<TList> ValidateCheckBoxes<TList>(string variable, string value, string group, IModel model)
		{
			if (value.IsEmpty())
				AddMessage(variable, group, "EmptySelection", MessageType.Validation, new MessageCollection() { { "Variable", variable, true } }, model);

			return value.ToCollection<TList>(',');
		}
		public Collection<TList> ValidateCheckBoxes<TList>(string variable, string value, IModel model)
		{
			return ValidateCheckBoxes<TList>(variable, value, "General", model);
		}
		public Collection<TList> ValidateCheckBoxes<TList>(string variable, string value)
		{
			return ValidateCheckBoxes<TList>(variable, value, "General", Model);
		}
		public Collection<TList> ValidateCheckBoxes<TList>(string variable, string value, string group)
		{
			return ValidateCheckBoxes<TList>(variable, value, group, Model);
		}
		#endregion

		#region Validate File
		public HttpPostedFile ValidateFile(string variable, HttpPostedFile uploadedFile, string[] extensions, string group, IModel model)
		{
			if (uploadedFile.IsNull())
				AddMessage(variable, group, "MissingFile", MessageType.Validation, new MessageCollection() { { "Variable", variable, true } }, model);

			else
			{
				if (uploadedFile.ContentLength == 0)
					AddMessage(variable, group, "MissingFile", MessageType.Validation, new MessageCollection() { { "Variable", variable, true } }, model);

				else
				{
					bool IsInExtensions = false;
					foreach (string Extension in extensions ?? new string[0])
					{
						if (uploadedFile.FileName.ToUpperInvariant().EndsWith(Extension, StringComparison.OrdinalIgnoreCase)) IsInExtensions = true;

					}
					if (!IsInExtensions)
						AddMessage(variable, group, "BadFormat", MessageType.Validation, new MessageCollection() { { "Variable", variable, true } }, model);

				}
			}

			return uploadedFile;
		}
		public HttpPostedFile ValidateFile(string variable, HttpPostedFile uploadedFile, string[] extensions, IModel model)
		{
			return ValidateFile(variable, uploadedFile, extensions, "General", model);
		}
		public HttpPostedFile ValidateFile(string variable, HttpPostedFile uploadedFile, string[] extensions)
		{
			return ValidateFile(variable, uploadedFile, extensions, "General", Model);
		}
		public HttpPostedFile ValidateFile(string variable, HttpPostedFile uploadedFile, string[] extensions, string group)
		{
			return ValidateFile(variable, uploadedFile, extensions, group, Model);
		}
		#endregion
		#endregion

		public void SetTimeout(int seconds)
		{
			Server.ScriptTimeout = seconds;
		}

		protected string WrapWithTransactionSql(string query)
		{
			return General.TransactionSqlWrapper.ParameterizedFormat("{TransactionalQuery}", query);
		}

		public Database DB { get; set; }
		public bool ModelIsNew { get; set; }
		public M<T> M { get; set; }
		public T Model { get; set; }
		public HttpServerUtility Server { get; set; }
		public ControllerInfo ControllerInfo { get; set; }
	}
}