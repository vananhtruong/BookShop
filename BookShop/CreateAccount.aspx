<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CreateAccount.aspx.cs" Inherits="BookShop.CreateAccount" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>Create Account</title>
    <link href="~/Content/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="container mt-5">
            <div class="row justify-content-center">
                <div class="col-md-6">
                    <div class="card shadow">
                        <div class="card-header bg-primary text-white">
                            <h3 class="mb-0">Create Account</h3>
                        </div>
                        <div class="card-body">
                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" CssClass="text-danger alert alert-danger" />

                            <div class="form-group">
                                <label for="txtFullName" class="form-label">Full Name</label>
                                <asp:TextBox ID="txtFullName" runat="server" CssClass="form-control" Placeholder="Enter full name"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtFullName"
                                    ErrorMessage="Full Name is required." CssClass="text-danger"></asp:RequiredFieldValidator>
                            </div>

                            <div class="form-group">
                                <label for="txtEmail" class="form-label">Email</label>
                                <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" Placeholder="Enter email"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtEmail"
                                    ErrorMessage="Email is required." CssClass="text-danger"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="EmailValidator" runat="server" ControlToValidate="txtEmail"
                                    ErrorMessage="Invalid Email format." ValidationExpression="\w+@[a-zA-Z_]+?\.[a-zA-Z]{2,6}" CssClass="text-danger"></asp:RegularExpressionValidator>
                            </div>

                            <div class="form-group">
                                <label for="txtPassword" class="form-label">Password</label>
                                <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="form-control" Placeholder="Enter password"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtPassword"
                                    ErrorMessage="Password is required." CssClass="text-danger"></asp:RequiredFieldValidator>
                            </div>

                            <div class="form-group">
                                <label for="txtConfirmPassword" class="form-label">Confirm Password</label>
                                <asp:TextBox ID="txtConfirmPassword" runat="server" TextMode="Password" CssClass="form-control" Placeholder="Confirm password"></asp:TextBox>
                                <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="txtConfirmPassword"
                                    ControlToCompare="txtPassword" ErrorMessage="Passwords do not match." CssClass="text-danger"></asp:CompareValidator>
                            </div>

                            <asp:Button ID="btnCreateAccount" runat="server" Text="Create Account" CssClass="btn btn-primary w-100" OnClick="btnCreateAccount_Click" />

                            <asp:Label ID="lblMessage" runat="server" CssClass="text-danger mt-3" Visible="false"></asp:Label>
                        </div>
                        <div class="card-footer text-center">
                            <p>Already have an account? <a href="Login.aspx" class="text-primary">Login</a></p>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
    <script src="~/Scripts/jquery/jquery.min.js"></script>
    <script src="~/Content/bootstrap/js/bootstrap.bundle.min.js"></script>
</body>
</html>
