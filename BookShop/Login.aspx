<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="BookShop.Login" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login</title>
    <link href="~/Content/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <link href="~/Content/LoginStyle.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="login-container">
            <h2>Login</h2>

            <asp:ValidationSummary ID="ValidationSummary1" runat="server" CssClass="alert alert-danger" />

            <div class="form-group">
                <asp:Label ID="Label1" runat="server" Text="Email:" AssociatedControlID="txtEmail"></asp:Label>
                <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtEmail"
                    ErrorMessage="Email is required." CssClass="text-danger"></asp:RequiredFieldValidator>
            </div>

            <div class="form-group">
                <asp:Label ID="Label2" runat="server" Text="Password:" AssociatedControlID="txtPassword"></asp:Label>
                <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="form-control"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtPassword"
                    ErrorMessage="Password is required." CssClass="text-danger"></asp:RequiredFieldValidator>
            </div>

            <asp:Button ID="btnLogin" runat="server" Text="Login" CssClass="btn btn-primary btn-block" OnClick="btnLogin_Click" />

            <asp:Label ID="lblMessage" runat="server" CssClass="text-danger" Visible="false"></asp:Label>

            <asp:Button ID="btnCreateAccount" runat="server" Text="Create Account" CssClass="btn btn-secondary btn-block btn-create-account"
                OnClick="btnCreateAccount_Click" CausesValidation="false" />

        </div>
    </form>

    <script src="~/Scripts/jquery/jquery.min.js"></script>
    <script src="~/Content/bootstrap/js/bootstrap.bundle.min.js"></script>
</body>
</html>
