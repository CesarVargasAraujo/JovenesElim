using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Ding.Core;

public partial class _Main : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
			string[] Directorires = Request.Path.Split('/');
			Page = Directorires.Last();
			Title = Dictionary.S(Page.ToLower());
			Host = General.Host;
    }
		public string Page;
		public string Title;
		public string Host;
}
