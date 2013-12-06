using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using Ding.Core;

namespace Elim.Core
{
	public partial class ControllerWS : System.Web.UI.Page
	{
		public static MSSql DB
		{
			get
			{
				return new MSSql(ConfigurationManager.ConnectionStrings["DingConnectionString"].ConnectionString);
			}
		}

		public ControllerWS()
		{

		}
	}
}
