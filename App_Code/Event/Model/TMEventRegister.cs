using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using Ding.Core;
using Ding.Enums;
using Ding.Interfaces;
using Ding.Structures;

namespace Elim.Event
{
	#region Table definition
	[ColumnInfoAttribute(0, "Event", ColumnDataType.Id, false, false, false, ColumnFilterExtras.None, Visible = false)]
	[ColumnInfoAttribute(1, "Numeration", ColumnDataType.Text, false, false, false, ColumnFilterExtras.None, ShowColumnName = false)]
	[ColumnInfoAttribute(2, "Sector", ColumnDataType.List, true, true, false, ColumnFilterExtras.FullSearch)]
	[ColumnInfoAttribute(3, "Church", ColumnDataType.List, true, true, false, ColumnFilterExtras.FullSearch)]
	[ColumnInfoAttribute(4, "Name", ColumnDataType.Text, true, true, true, ColumnFilterExtras.PartialSearch)]
	[ColumnInfoAttribute(5, "Age", ColumnDataType.Text, false, true, false, ColumnFilterExtras.None)]
	[ColumnInfoAttribute(6, "Email", ColumnDataType.Text, false, false, false, ColumnFilterExtras.None)]
	[ColumnInfoAttribute(7, "Cooperation", ColumnDataType.Text, false, false, false, ColumnFilterExtras.None)]
	#endregion

	[Serializable]
	public class TDEventRegister
	{
		#region Properties
		public Guid Event;
		public int Numeration;
		public string Sector;
		public string Church;
		public string Name;
		public string Age;
		public string Email;
		public string Cooperation;
		#endregion
	}

	[Serializable]
	public class TMEventRegister : Model, IDataTable<TDEventRegister>
	{
		public string OrderBy { get; set; }
		public string OrderByDirection { get; set; }
		public string OrderType { get; set; }
		public string OrderByExtra { get; set; }
		public int CurrentPage { get; set; }
		public int TotalPages { get; set; }
		public int PagesPerPage { get; set; }
		public int PaginationPages { get; set; }
		public bool NoRowLimit { get; set; }
		public Collection<TDEventRegister> Results { get; set; }
		public Dictionary<string, ColumnListFilterCollection> Lists { get; set; }
		public TDEventRegister Filter { get; set; }
	}
}
