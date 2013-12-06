using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;

namespace Elim.Core
{
	[Serializable]
	public class TableData
	{
		public string OrderBy;
		public string OrderByDirection;
		public int CurrentPage;
		public int PagesPerPage;
		public int PaginationPages;
		public bool NoRowLimit;
		public Collection<TableFilter> Filters;

		public TableData() : this("", "", 20, 9, 1, false, new Collection<TableFilter>()) { }
		public TableData(string orderBy, string orderByDirection, int pagesPerPage, int paginationPages, int currentPage, bool noRowLimit, Collection<TableFilter> filters)
		{
			this.OrderBy = orderBy;
			this.OrderByDirection = orderByDirection;
			this.PagesPerPage = pagesPerPage;
			this.PaginationPages = paginationPages;
			this.CurrentPage = currentPage;
			this.NoRowLimit = noRowLimit;
			this.Filters = filters;
		}
	}

	[Serializable]
	public class TableFilter
	{
		public string Key;
		public string Value;

		public TableFilter() : this(null, null) { }
		public TableFilter(string key, string value)
		{
			this.Key = key;
			this.Value = value;
		}
	}
}