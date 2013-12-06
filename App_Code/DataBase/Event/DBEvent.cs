using System;
using System.Collections.Generic;
using System.Data;
using Ding.Core;

namespace Elim.Event
{
	public class DBEvent
	{
		private Database DB;
		public DBEvent(Database DataBase) { DB = DataBase; }

		#region Get
		public PMEvent Get(Guid EventId)
		{
			return Get(DB.GetRow("EvetById", "@Event", EventId));
		}

		private PMEvent Get(DataRow Row)
		{
			if (Row.B("Exists"))
				return new PMEvent(Row.G("Event"), Row.S("Name"), Row.DT("Date"), Row.D("Cost"), Row.NullG("Church"), Row.NullG("Place"));
			else
				return new PMEvent();
		}
		#endregion

		#region Insert
		public void InsertAssistence(Guid Event, Guid Young, decimal Cooperation)
		{
			DB.Insert("EventAssistence"
				, "@Event", Event
				, "@Young", Young
				, "@Cooperation", Cooperation);
		}
		#endregion
	}
}