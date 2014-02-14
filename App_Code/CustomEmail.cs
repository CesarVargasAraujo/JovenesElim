
using Ding.Core;

namespace Gestionix.Core
{
	public class CustomEmail : Ding.Core.Email
	{
		private Database DB = Database.New();
		public CustomEmail() : base() {  }


		public void SendAdvertising(string Email, string Name)
		{
			string Subject = "Renovando el compromiso por Amor";
			string Body = @"
			<table style='text-align: left; width: 600px; font-family: Segoe UI,Calibri,Arial,Verdana; font-size: 15px; color: #555555;' border='0' cellspacing='0px' cellpadding='0px'>
				<thead>
					<tr>
						<td><a style='text-decoration: none; border: none;' href='http://jovenes.elim/'>
							<img style='border-bottom: 2px solid #8B5A25;' src='http://joveneselim.com.mx/Image/_Main/Email-Logo.png' alt='Joven&eacute;s Elim' /></a></td>
						<td style='border-bottom: 2px solid #8B5A25; border-right: 2px solid #8B5A25; color: #8b5a25; font-size: 15px; padding: 5px; text-align: center; vertical-align:middle; width: 595px;'>Ninguno tenga en poco tu juventud, sino s&eacute; ejemplo de los creyentes en palabra, conducta, amor, esp&iacute;ritu, fe y pureza&rdquo; 1Timoteo 4:12</td>
						<td>
							<img style='border-bottom: 2px solid #8B5A25;' src='http://joveneselim.com.mx/Image/_Main/Email-Eslogan.png' alt='Regresando al camino del Se&ntilde;or' /></td>
					</tr>
				</thead>
				<tbody>
					<tr>
						<td style='padding: 10px 10px 20px 30px; text-align: left;' colspan='3' valign='middle'>
							<p>&iexcl;Hola <strong>{Name}</strong>!<br />
								Bendiciones en nuestro Se&ntilde;or Jesucristo.</p>
							<p>Esperamos que nuestro Dios llene tu vida de bendiciones, te hacemos llegar la informaci&oacute;n para nuestra pr&oacute;xima actividad a nivel Federaci&oacute;n, <strong>'Renovando el compromiso por Amor'</strong>. La actividad ser&aacute; de gran bendici&oacute;n esperamos contar con tu asistencia. Que Dios te siga bendiciendo.</p>
						</td>
					</tr>
					<tr>
						<td style='text-align: center;' colspan='3' valign='middle'>
							<p><a style='text-decoration: none; border: none;' href='http://joveneselim.com.mx/'>
								<img src='http://joveneselim.com.mx/Image/Advertising/20140215.jpg' alt='Joven&eacute;s Elim' /></a></p>
						</td>
					</tr>
					<tr>
						<td style='padding: 10px 10px 20px 30px; text-align: left;' colspan='3' valign='middle'>
							<p>Por favor ay&uacute;danos a difundir esta actividad. Grax!!!
								<br /> Para mas informaci&oacute;n visita nuestra pagina <a style='text-decoration: none; border: none;' href='http://joveneselim.com.mx/'>http://joveneselim.com.mx</a></p>
						</td>
					</tr>
				</tbody>
			</table>".ParameterizedFormat("{Name}", Name);

			Send(Email, Subject, Body);
		}
	}
}