using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Elim.Core
{
	public static class CustomGeneral
	{
		#region Regex
		public static Regex RegexEmail = new Regex(@"[A-Za-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[A-Za-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[A-Za-z0-9](?:[A-Za-z0-9-]*[A-Za-z0-9])?\.)+[A-Za-z0-9](?:[A-Za-z0-9-]*[A-Za-z0-9])?", RegexOptions.Compiled);
		public static Regex RegexPhone = new Regex(@"^[0-9-() ]{1,15}$", RegexOptions.Compiled);
		#endregion

		#region Options Mexico States
		public static List<string> MexicoStates = new List<string>() { "Aguascalientes", "Baja|California", "Baja|California|Sur", "Campeche", "Chiapas", "Chihuahua", 
            "Coahuila", "Colima", "Distrito|Federal", "Durango", "Estado|De|México", "Guanajuato", "Guerrero", "Hidalgo", "Jalisco", "Michoacán", "Morelos", "Nayarit", 
            "Nuevo|León", "Oaxaca", "Puebla", "Querétaro", "Quintana|Roo", "San|Luis|Potosí", "Sinaloa", "Sonora", "Tabasco", "Tamaulipas", "Tlaxcala", "Veracruz", 
            "Yucatan", "Zacatecas" };
		#endregion

		#region Html Format
		public static string FormatHtml(this string html)
		{
			return html.Replace("\t", String.Empty).Replace("\r", String.Empty).Replace("\n", String.Empty);
		}
		#endregion
	}
}