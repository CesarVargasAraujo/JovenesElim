using System.Collections.Generic;
using System.Data;

using Ding.Core;

namespace Elim.Young
{
	public class DBYoung
	{
		private Database DB;
		public DBYoung(Database DataBase) { DB = DataBase; }

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
	}
}