using System;
using Ding.Core;

namespace Elim.Young
{
	[Serializable]
	public class PMYoung
	{
		#region Properties
		public Guid? YoungId;
		public Guid? SectorId;
		public Guid? ChurchId;
		public string Sector;
		public string Church;
		public string Name;
		public string Surnames;
		public string Email;
		public string Facebook;
		public DateTime? Birthday;
		public DateTime? RegisterDate;
		#endregion

		#region Contructor
		public PMYoung() : this(null, null, null, null, null, null, null, null, null, null, null) { }

		public PMYoung(Guid youngId, Guid sectorId, Guid churchId, string sector, string church, string name, string surnames, string email, string facebook, DateTime birthday, DateTime registerDate)
			: this(youngId.NullG(), sectorId.NullG(), churchId.NullG(), sector, church, name, surnames, email, facebook, birthday, registerDate) { }

		private PMYoung(Guid? youngId, Guid? sectorId, Guid? churchId, string sector, string church, string name, string surnames, string email, string facebook, DateTime? birthday, DateTime? registerDate)
		{
			this.YoungId = youngId;
			this.SectorId = sectorId;
			this.ChurchId = churchId;
			this.Sector = sector;
			this.Church = church;
			this.Name = name;
			this.Surnames = surnames;
			this.Email = email;
			this.Facebook = facebook;
			this.Birthday = birthday;
			this.RegisterDate = registerDate;
		}
		#endregion
	}
}