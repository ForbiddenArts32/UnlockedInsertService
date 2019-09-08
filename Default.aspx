<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="UnlockedInsertService.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Unlocked InsertService Server for Roblox</title>
    <link rel="stylesheet" type="text/css" href="style.css" />
</head>
<body>
    <div class="UiContentBox">
        <% if (_LoggedInUserId.HasValue) { %>
        <h1>You're good to go!</h1>
        <p>You can now use this server to bypass <a href="https://developer.roblox.com/en-us/api-reference/function/InsertService/LoadAsset">InsertService restrictions</a>!</p>
        <p>To use this in your game, <a href="https://www.roblox.com/catalog/198858951">take the Model</a> and put it into your game's ServerScriptService. Then configure it to point at this server.</p>
        <p><strong>Your user ID is <%=_LoggedInUserId.Value.ToString() %></strong></p>
        <% } else { %>
        <h1>The server cannot log into Roblox.</h1>
        <p>The server is unable to access Roblox on your behalf. Please use your server's admin console and edit the <strong>Web.config</strong> file with your current ROBLOSECURITY cookie.</p>
        <% } %>
    </div>
</body>
</html>
