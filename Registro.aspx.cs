using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ding.Core;
using Elim.Church;
using Elim.Event;
using Elim.Young;

public partial class Registro : System.Web.UI.Page
{
	protected void Page_Load(object sender, EventArgs e)
	{
		Database DB = Database.New();

		#region Event
		DBEvent DBEvent = new DBEvent(DB);
		Event = DBEvent.Get(new Guid("5F014EA2-3515-4B63-9989-F68A01043E72"));
		#endregion

		#region Search
		DBYoung DBYoung = new DBYoung(DB);
		SearchOptions = new StringBuilder();

		foreach (PMYoung Young in DBYoung.Gets())
			SearchOptions.AppendParameterizedFormat(@"
			<option value='{YoungId}' ChurchId='{ChurchId}' Name='{Name}' Surnames='{Surnames}' Email='{Email}' Facebook='{Facebook}' Birthday='{Birthday}'>{Sector} - {Church} - {CompleteName}</option>",
			"{YoungId}", Young.YoungId,
			"{ChurchId}", Young.ChurchId,
			"{Sector}", Young.Sector,
			"{Church}", Young.Church,
			"{CompleteName}", Young.Name + " " + Young.Surnames,
			"{Name}", Young.Name,
			"{Surnames}", Young.Surnames,
			"{Email}", Young.Email,
			"{Facebook}", Young.Facebook,
			"{Birthday}", Young.Birthday.FormatDate());
		#endregion

		#region Churchs
		DBChurch DBChurch = new DBChurch(DB);
		ChurchsOptions = new StringBuilder();

		foreach (PMChurch Church in DBChurch.Gets())
			ChurchsOptions.AppendParameterizedFormat(@"<option value='{ChurchId}'>{Municipality} - {Name}</option>",
			"{ChurchId}", Church.ChurchId,
			"{Municipality}", Church.Municipality,
			"{Name}", Church.Name);
		#endregion

		SendEmail();
	}

	private void SendEmail() {
		//foreach (DataRow Row in Database.New().GetTableFromQuery("SELECT * FROM Young WHERE Email IS NOT NULL").Rows)
		//	new Gestionix.Core.CustomEmail().SendAdvertising(Row.S("Email"), Row.S("Name"));

		new Gestionix.Core.CustomEmail().SendAdvertising("cesar.vargas@abatta.com", "Cesar");
	}

	public PMEvent Event;
	public StringBuilder SearchOptions;
	public StringBuilder ChurchsOptions;
}