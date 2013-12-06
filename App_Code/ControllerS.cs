using System.Collections.Generic;
using System.Web.SessionState;

using Ding.Core;
using Ding.Interfaces;
using Ding.Structures;

namespace Elim.Core
{
	public class ControllerS<T> : Controller<T> where T : class, IModel, new()
	{
		//public LoggedUser LoggedUser;
		public HttpSessionState Session;

		public ControllerS()
			: base()
		{
			//LoggedUser = new LoggedUser();
			base.InitializeControllerEvents();
			ControllerInfo.PageName = ControllerInfo.PageName.ToLower();
		}

		#region Messages
		public string GetMessagesType()
		{
			List<string> messgeTypes = new List<string>();

			foreach (Message Message in Model.Messages)
				if (!messgeTypes.Contains(Message.MessageType.S()))
					messgeTypes.Add(Message.MessageType.S());

			return string.Join(",", messgeTypes.ToArray());
		}
		#endregion
	}
}