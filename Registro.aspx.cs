using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Ding.Core;
using Elim.Church;
using Elim.Event;

public partial class Registro : System.Web.UI.Page
{
	protected void Page_Load(object sender, EventArgs e)
	{
		Database DB =Database.New();

		#region Event
		DBEvent DBEvent = new DBEvent(DB);
		Event = DBEvent.Get(new Guid("7A31464F-9CE0-49BA-9A7C-09888D1B1749"));
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
	}

	public PMEvent Event;
	public StringBuilder ChurchsOptions;
}