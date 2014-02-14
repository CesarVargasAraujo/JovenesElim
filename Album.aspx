<%@ Page Title="" Language="C#" MasterPageFile="~/_Main.master" AutoEventWireup="true" CodeFile="Album.aspx.cs" Inherits="Album" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
  <link href="Css/Album.css" rel="stylesheet" />
  <script src="JS/_JQuery/JQuery.Glisse.js"></script>

  <!-- JS -->
  <script>
    $(function () {
      $('.tl').glisse({ speed: 500, changeSpeed: 550, effect: 'fade', fullscreen: false });
    });
  </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
  <table>
    <tr>
      <td>
        <ul class="stack" data-count="9">
          <li><img src="Image/Album/20131207/publi-s.jpg" width="250px" rel="group1" data-glisse-big="Image/Album/20131207/publi.jpg" class="tl" title="Lorem ipsum dolor sit amet, consectetuer adipiscing elit" /></li>
          <li><img src="Image/Album/20131207/DSC01183-s.jpg" width="250px" rel="group1" data-glisse-big="Image/Album/20131207/DSC01183.jpg" class="tl" title="Cum sociis natoque penatibus et magnis" /></li>
          <li><img src="Image/Album/20131207/DSC01184-s.jpg" width="250px" rel="group1" data-glisse-big="Image/Album/20131207/DSC01184.jpg" class="tl" title="Nascetur ridiculus mus" /></li>
          <li><img src="Image/Album/20131207/DSC01185-s.jpg" width="250px" rel="group1" data-glisse-big="Image/Album/20131207/DSC01185.jpg" class="tl" title="Nascetur ridiculus mus" /></li>
          <li><img src="Image/Album/20131207/DSC01186-s.jpg" width="250px" rel="group1" data-glisse-big="Image/Album/20131207/DSC01186.jpg" class="tl" title="Nascetur ridiculus mus" /></li>
          <li><img src="Image/Album/20131207/DSC01187-s.jpg" width="250px" rel="group1" data-glisse-big="Image/Album/20131207/DSC01187.jpg" class="tl" title="Nascetur ridiculus mus" /></li>
          <li><img src="Image/Album/20131207/DSC01188-s.jpg" width="250px" rel="group1" data-glisse-big="Image/Album/20131207/DSC01188.jpg" class="tl" title="Nascetur ridiculus mus" /></li>
          <li><img src="Image/Album/20131207/DSC01189-s.jpg" width="250px" rel="group1" data-glisse-big="Image/Album/20131207/DSC01189.jpg" class="tl" title="Nascetur ridiculus mus" /></li>
          <li><img src="Image/Album/20131207/DSC01190-s.jpg" width="250px" rel="group1" data-glisse-big="Image/Album/20131207/DSC011810.jpg" class="tl" title="Nascetur ridiculus mus" /></li>
        </ul>
      </td>
      <td></td>
      <td></td>
    </tr>
  </table>
  
<%--  <ul class="stack" data-count="5">
    <li>
      <img src="pictures/_MG_9330_t.jpg" rel="group3" data-glisse-big="pictures/_MG_9330.jpg" class="tl" title="Aenean commodo ligula eget dolor" /></li>
    <li>
      <img src="pictures/_MG_9365_t.jpg" rel="group3" data-glisse-big="pictures/_MG_9365.jpg" class="tl" title="Consectetuer adipiscing elit" /></li>
    <li>
      <img src="pictures/_MG_9395_t.jpg" rel="group3" data-glisse-big="pictures/_MG_9395.jpg" class="tl" title="Aenean massa" /></li>
    <li>
      <img src="pictures/_MG_9743_t.jpg" rel="group3" data-glisse-big="pictures/_MG_9743.jpg" class="tl" title="Lorem ipsum dolor sit amet" /></li>
    <li>
      <img src="pictures/_MG_9781_t.jpg" rel="group3" data-glisse-big="pictures/_MG_9781.jpg" class="tl" title="Dis parturient montes" /></li>
  </ul>--%>

        

</asp:Content>

