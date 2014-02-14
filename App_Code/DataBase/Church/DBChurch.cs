using System;
using System.Collections.Generic;
using System.Data;
using Ding.Core;

namespace Elim.Church
{
	public class DBChurch
	{
		private Database DB;
		public DBChurch(Database DataBase) { DB = DataBase; }

		#region Get
		public List<PMChurch> Gets()
		{
			return Gets(DB.GetTable("Churchs"));
		}

		public List<PMChurch> Gets(Guid EventId)
		{
			return Gets(DB.GetTable("ChurchsByEventId", "@Event", EventId));
		}

		private List<PMChurch> Gets(DataTable Table)
		{
			List<PMChurch> Data = new List<PMChurch>();
			foreach (DataRow Row in Table.Rows)
				Data.Add(new PMChurch(Row.G("Church"), Row.G("Sector"), Row.S("SectorName"), Row.G("Type"), Row.S("TypeDescription"), Row.S("Name"), Row.S("Municipality"), Row.S("Address"), Row.S("Phone")));

			return Data;
		}
		#endregion
	}
}