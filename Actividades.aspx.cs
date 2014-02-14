using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ding.Core;

public partial class Actividades : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
			//foreach (DataRow Row in Database.New().GetTableFromQuery("SELECT * FROM Young WHERE Email IS NOT NULL").Rows)
			//	new Gestionix.Core.CustomEmail().SendAdvertising(Row.S("Email"), Row.S("Name"));
    }
}