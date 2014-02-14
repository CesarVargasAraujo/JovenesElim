using System.Collections.Generic;
using System.Data;

using Ding.Core;

namespace Elim.Young
{
	public class DBYoung
	{
		private Database DB;
		public DBYoung(Database DataBase) { DB = DataBase; }

		#region Get
		public List<PMYoung> Gets()
		{
			return Gets(DB.GetTable("Youngs"));
		}

		private List<PMYoung> Gets(DataTable Table)
		{
			List<PMYoung> Data = new List<PMYoung>();
			foreach (DataRow Row in Table.Rows)
				Data.Add(new PMYoung(Row.G("Young"), Row.G("Sector"), Row.G("Church"), Row.S("SectorName"), Row.S("ChurchName"), Row.S("Name"), Row.S("Surnames"), Row.S("Email"), Row.S("Facebook"), Row.DT("Birthday"), Row.DT("RegisterDate")));

			return Data;
		}
		#endregion

		#region Insert
		public void Insert(PMYoung Young)
		{
			DB.Insert("Young"
				, "@Young", Young.YoungId
				, "@Church", Young.ChurchId
				, "@Name", Young.Name
				, "@Surnames", Young.Surnames
				, "@Email", Young.Email
				, "@Facebook", Young.Facebook
				, "@Birthday", Young.Birthday);
		}
		#endregion

		#region Insert
		public void Update(PMYoung Young)
		{
			DB.Update("Young"
				, "@Young", Young.YoungId
				, "@Church", Young.ChurchId
				, "@Name", Young.Name
				, "@Surnames", Young.Surnames
				, "@Email", Young.Email
				, "@Facebook", Young.Facebook
				, "@Birthday", Young.Birthday);
		}
		#endregion
	}
}