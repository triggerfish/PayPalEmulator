<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<System.Web.Mvc.HandleErrorInfo>" %>

<asp:Content ID="errorTitle" ContentPlaceHolderID="TitleContent" runat="server">Error</asp:Content>

<asp:Content ID="errorContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Error</h2>
    <table>
    <tbody>
        <tr>
            <td>Controller</td>
            <td><%= Model.ControllerName %></td>
        </tr>
        <tr>
            <td>Action</td>
            <td><%= Model.ActionName %></td>
        </tr>
        <tr>
            <td>Message</td>
            <td><%= Model.Exception.Message %></td>
        </tr>
    </tbody>
    </table>
</asp:Content>
