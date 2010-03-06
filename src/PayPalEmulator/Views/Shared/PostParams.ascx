<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
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

