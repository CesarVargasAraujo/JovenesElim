using System;
using Ding.Core;

namespace Elim.Church
{
	[Serializable]
	public class PMChurch
	{
		#region Properties
		public Guid? ChurchId;
		public Guid? SectorId;
		public string Sector;
		public Guid? TypeId;
		public string Type;
		public string Name;
		public string Municipality;
		public string Address;
		public string Phone;
		#endregion

		#region Contructor
		public PMChurch() : this(null, null, null, null, null, null, null, null, null) { }

		public PMChurch(Guid churchId, Guid sectorId, string sector, Guid typeId, string type, string name, string municipality, string address, string phone)
			: this(churchId.NullG(), sectorId.NullG(), sector, typeId.NullG(), type, name, municipality, address, phone) { }

		private PMChurch(Guid? churchId, Guid? sectorId, string sector, Guid? typeId, string type, string name, string municipality, string address, string phone)
		{
			this.ChurchId = churchId;
			this.SectorId = sectorId;
			this.Sector = sector;
			this.TypeId = typeId;
			this.Type = type;
			this.Name = name;
			this.Municipality = municipality;
			this.Address = address;
			this.Phone = phone;
		}
		#endregion
	}
}