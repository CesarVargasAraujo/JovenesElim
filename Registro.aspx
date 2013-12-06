<%@ Page Title="" Language="C#" MasterPageFile="~/_Main.master" AutoEventWireup="true" CodeFile="Registro.aspx.cs" Inherits="Registro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
  <%--Css--%>
  <link href="Css/Register.css" rel="stylesheet" />
  
 <%--JS--%>
  <script src="JS/Event/Table.js"></script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
  <div class="main-container register-body">
    <div class="register-header">
      <div class="register-title"><%=Event.Name %></div>
      <div class="register-date"><%=Dictionary.S("FormatLargeDate", new MessageCollection() { { "day", Event.Date.DT().Day }, { "month", Event.Date.DT().MonthName(), true }, { "year", Event.Date.DT().Year } })%></div>
    </div>
    <div class="general-panel">
      <div class="general-panel-title"><%=Dictionary.S("General")%></div>
      <div class="MessageContainer AutomaticClose"></div>

      <table>
        <tr>
          <td><%=Dictionary.S("Name")%></td>
          <td><input type="text" /></td>
          <td><%=Dictionary.S("Surnames")%></td>
          <td><input type="text" /></td>
        </tr>
        <tr>
          <td><%=Dictionary.S("Email")%></td>
          <td><input type="text" /></td>
          <td><%=Dictionary.S("Facebook")%></td>
          <td><input type="text" /></td>
        </tr>
        <tr>
          <td><%=Dictionary.S("Birthday")%></td>
          <td><input type="text" class="datepicker"/></td>
          <td><%=Dictionary.S("Church")%></td>
          <td><select class="combobox"><option value="">---</option><%=ChurchsOptions %></select></td>
        </tr>
        <tr>
          <td><%=Dictionary.S("Cooperation")%></td>
          <td><input type="text" class="money" value="<%=Event.Cost %>" /></td>
          <td colspan="2"><input type="button" class="ui-button ui-widget ui-state-default ui-corner-all ui-button-text-only" value="<%=Dictionary.S("Save") %>" /></td>
        </tr>
      </table>
    </div>

    <div class="general-table">
      <div id="ERTTitle" class="general-table-title" ><%=Dictionary.S("Attendance") %></div>
      <div id="ERTCover" class="general-cover"></div>
      <div id="ERTContainer"></div>
    </div>
  </div>
</asp:Content>
