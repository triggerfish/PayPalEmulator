<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<PaymentViewData>" %>

<asp:Content ID="Title" ContentPlaceHolderID="TitleContent" runat="server">MakePayment</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Make Payment</h2>
    IPN is <%= Model.IsIpnEnabled ? "enabled" : "disabled" %>
    <% using (Html.BeginForm("PayNow", "BuyNow")) { %>
        <fieldset>
            <%= Html.RadioButton("action", "Succeed", true, new { id = "action" }) %><label for="Succeed">Succeed</label><br />
            <%= Html.RadioButton("action", "Fail", false, new { id = "action" }) %><label for="Fail">Fail</label><br />
            <%= Html.RadioButton("action", "Corrupt", false, new { id = "action" })%><label for="Corrupt">Corrupt Response</label><br />
            <%= Html.Hidden("pdtId", Model.Tx.Id)%>
            <input type="submit" value="Pay Now" />
        </fieldset>
    <% } %>
</asp:Content>
