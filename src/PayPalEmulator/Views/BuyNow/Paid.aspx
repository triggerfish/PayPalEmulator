<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<PaymentViewData>" %>

<asp:Content ID="Title" ContentPlaceHolderID="TitleContent" runat="server">Paid</asp:Content>

<asp:Content ID="Head" ContentPlaceHolderID="HeadContent" runat="server"><%= String.Format("<META http-equiv=\"REFRESH\" content=5;url=\"{0}\">", Model.Tx.ToPdtReturnUrl())%></asp:Content>

<asp:Content ID="Content" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Paid</h2>
    <p>PayPal transaction: <%= Model.Tx.Tx %></p>
    <% if (Model.Tx.State == "Completed") { %>
    <p>Thank you, your payment has been received.</p>
    <% } else { %>
    <p>Sorry, payment failed.</p>
    <% } %>
    <p>You will be redirected to the vendor's site in 5 seconds.</p>
</asp:Content>
