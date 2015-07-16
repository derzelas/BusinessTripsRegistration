<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Error401.aspx.cs" Inherits="BusinessTrips.Error401" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div class="col-md-4">
            <h3>Sorry, you need authorization in order to access this feature!</h3>
            <a href="UserOperations/Login">Login</a> |
            <a href="UserOperations/Register">Register</a>
        </div>
    </form>
</body>
</html>
