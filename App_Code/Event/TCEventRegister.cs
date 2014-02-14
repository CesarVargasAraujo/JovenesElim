using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using Ding.Core;
using Ding.Structures;
using Elim.Church;
using Elim.Core;
using Elim.Sector;

namespace Elim.Event
{
	public class TCEventRegister
	{
		private Database DB;
		private DBSector DBSector;
		private DBChurch DBChurch;

		public TCEventRegister() : this(Database.New()) { }
		public TCEventRegister(Database db)
		{
			this.DB = db;
			this.DBSector = new DBSector(DB);
			this.DBChurch = new DBChurch(DB);
		}

		public TMEventRegister GetTable(TMEventRegister Model, TableData TableData)
		{
			Model.PagesPerPage = 20;
			Model.CurrentPage = TableData.CurrentPage;
			Model.OrderBy = TableData.OrderBy;
			Model.OrderByDirection = TableData.OrderByDirection;
			Model.NoRowLimit = true;
			Guid EventId = new Guid("5F014EA2-3515-4B63-9989-F68A01043E72");

			#region systemFilters
			Dictionary<string, string> systemFilters = new Dictionary<string, string>();
			foreach (TableFilter Filter in TableData.Filters)
				systemFilters.Add(Filter.Key, Filter.Value);
			systemFilters.Add("Event", EventId.S());
			#endregion

			#region sqlFilters
			string sqlFilters = "";
			#endregion

			#region Table
			DataSet PagedData = DB.GetPagedData<TDEventRegister>("VEventAssistence", Model, systemFilters, sqlFilters);
			Model.TotalPages = PagedData.Tables[0].Rows[0].I("TotalPages");
			DataTable Table = PagedData.Tables[1];
			Model.Results = new Collection<TDEventRegister>();

			foreach (DataRow Row in Table.Rows)
			{
				Model.Results.Add(new TDEventRegister()
				{
					Event = Row.G("Event"),
					Sector = Row.G("Sector"),
					Church = Row.G("Church"),
					Young = Row.G("Young"),
					Numeration = Table.Rows.IndexOf(Row)+1,
					SectorName = Row.S("SectorName"),
					ChurchName = Row.S("ChurchName"),
					Name = Row.S("Name"),
					Age = Row.S("Age"),
					Email = Row.S("Email"),
					Cooperation = Row.S("Cooperation").FormatCurrency()
				});
			}
			#endregion

			#region FiltersList
			Model.Lists = new Dictionary<string, ColumnListFilterCollection>();

			Model.Lists.Add("SectorName", new ColumnListFilterCollection());
			foreach (KeyValuePair<Guid, string> Sector in DBSector.Gets(EventId))
				Model.Lists["SectorName"].Add(Sector.Value);

			Model.Lists.Add("ChurchName", new ColumnListFilterCollection());
			foreach (PMChurch Church in DBChurch.Gets(EventId))
				Model.Lists["ChurchName"].Add(Church.Name);
			#endregion

			return Model;
		}
	}
}