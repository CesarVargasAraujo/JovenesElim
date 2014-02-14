using System;
using System.Collections.Generic;
using System.Data;
using Ding.Core;

namespace Elim.Sector
{
	public class DBSector
	{
		private Database DB;
		public DBSector(Database DataBase) { DB = DataBase; }

		#region Get
		public Dictionary<Guid,string> Gets(Guid EventId)
		{
			return Gets(DB.GetTable("Sectors", "@Event", EventId));
		}

		private Dictionary<Guid, string> Gets(DataTable Table)
		{
			Dictionary<Guid, string> Data = new Dictionary<Guid, string>();
			foreach (DataRow Row in Table.Rows)
				Data.Add(Row.G("Sector"), Row.S("Name"));

			return Data;
		}
		#endregion
	}
}