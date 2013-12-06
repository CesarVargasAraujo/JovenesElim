using System;
using Ding.Core;

namespace Elim.Event
{
	[Serializable]
	public class PMEvent
	{
		#region Properties
		public Guid? EventId;
		public string Name;
		public DateTime? Date;
		public decimal? Cost;
		public Guid? ChurchId;
		public Guid? PlaceId;
		#endregion

		#region Contructor
		public PMEvent() : this(null, null, null, null, null, null) { }

		public PMEvent(Guid? eventId, string name, DateTime date, decimal cost, Guid? churchId, Guid? placeId)
			: this(eventId.NullG(), name, date, cost.NullD(), churchId, placeId) { }

		private PMEvent(Guid? eventId, string name, DateTime? date, decimal? cost, Guid? churchId, Guid? placeId)
		{
			this.EventId = eventId;
			this.Name = name;
			this.Date = date;
			this.Cost = cost;
			this.ChurchId = churchId;
			this.PlaceId = placeId;
		}
		#endregion
	}
}