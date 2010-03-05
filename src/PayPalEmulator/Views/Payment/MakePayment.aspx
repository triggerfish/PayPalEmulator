<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<PaymentViewData>" %>

<asp:Content ID="Title" ContentPlaceHolderID="TitleContent" runat="server">MakePayment</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Make Payment</h2>
    <p>Post variables:</p>
    <table>
    <tbody>
    <% foreach (string key in Request.Form.AllKeys) { %>
        <tr>
            <td><%= key %></td>
            <td><%= Request.Form[key] %></td>
        </tr>
    <% } %>
    </tbody>
    </table>

    <% using (Html.BeginForm("PayNow", "Payment")) { %>
        <fieldset>
            <%= Html.Hidden("pdtId", Model.PDT.Id)%>
            <input type="submit" value="Pay Now" />
        </fieldset>
    <% } %>
</asp:Content>
