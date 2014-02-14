using System;
using System.Web.Script.Services;
using System.Web.Services;
using Ding.Core;
using Elim.Core;
using Elim.Event;

public partial class WebMethods_Event : ControllerWS
{
  private static PCEvent PCEvent;

	#region Table
	[WebMethod()]
	[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
	public static string GetTable(TableData TableData)
	{
		Controller<TMEventRegister> C = new Controller<TMEventRegister>();
		if (TableData == null) TableData = new TableData();
		C.Model = new TCEventRegister(DB).GetTable(C.Model, TableData);

		Ding.Controls.DataTable DataTable = new Ding.Controls.DataTable();
		DataTable.ID = "ERTTable";
		DataTable.CreateDataTable<TDEventRegister>(C.Model);

		return DataTable.HtmlDataTableBuilder.S().FormatHtml();
	}
	#endregion

  #region Register
  [WebMethod()]
  [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
	public static string RegisterYoungAssistance(Guid EventId, Guid? YoungId, string ChurchId, string Name, string Surnames, string Email, string Facebook, string BirthDay, decimal Cooperation)
  { 
   PCEvent = new PCEvent();
	 PCEvent.RegisterAssistance(EventId, YoungId, ChurchId == "" ? null : new Guid(ChurchId).NullG(), Name, Surnames, Email, Facebook, BirthDay == "" ? null : BirthDay.NullDT(), Cooperation);
   return PCEvent.GetMessagesFormatJson();
  }
  #endregion
}