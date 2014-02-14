<%@ Page Title="" Language="C#" MasterPageFile="~/_Main.master" AutoEventWireup="true" CodeFile="Actividades.aspx.cs" Inherits="Actividades" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
  <link href="Css/Activities.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
  <table class="advertising">
    <tr>
      <td class="advertising-img" rowspan="2">
        <img src="Image/Advertising/20140215.jpg" /></td>
      <td class="advertising-church-img">
        <img src="Image/Church/Cuernavaca_PrincipedePaz.jpg" width="93%" />
      </td>
      <td class="advertising-church-data">
        <div class="advertising-church-title">"Príncipe de Paz"</div>
        Dirección: 
        <ul>
          <li>Calle Dalia No. 12</li>
          <li>Col. Satélite, 62030</li>
          <li>Cuernavaca, Morelos</li>
          <li>+52 777 315 0910</li>
        </ul>
        <small><a href="https://maps.google.com/maps?f=q&amp;source=embed&amp;hl=es&amp;geocode=&amp;q=Iglesia+Bautista+Principe+de+Paz+cuernavaca&amp;aq=&amp;sll=18.91271,-99.205123&amp;sspn=0.003649,0.006539&amp;ie=UTF8&amp;hq=Iglesia+Bautista+Principe+de+Paz&amp;hnear=Cuernavaca,+Morelos,+M%C3%A9xico&amp;t=m&amp;cid=1414490683842039140&amp;ll=18.922526,-99.205341&amp;spn=0.029229,0.047121&amp;z=14&amp;iwloc=A" target="_blank" style="color:#0000FF;text-align:left; float: right;">Ver mapa más grande</a></small>
      </td>
    </tr>
    <tr>
      <td class="advertising-map" colspan="2">
        <iframe width="570" height="330" frameborder="0" scrolling="no" marginheight="0" marginwidth="0" src="https://maps.google.com/maps?f=q&amp;source=s_q&amp;hl=es&amp;geocode=&amp;q=Iglesia+Bautista+Principe+de+Paz+cuernavaca&amp;aq=&amp;sll=18.91271,-99.205123&amp;sspn=0.003649,0.006539&amp;ie=UTF8&amp;hq=Iglesia+Bautista+Principe+de+Paz&amp;hnear=Cuernavaca,+Morelos,+M%C3%A9xico&amp;t=m&amp;cid=1414490683842039140&amp;ll=18.922526,-99.205341&amp;spn=0.029229,0.047121&amp;z=14&amp;iwloc=A&amp;output=embed"></iframe>
      </td>
    </tr>
  </table>

</asp:Content>

