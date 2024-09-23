<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ViewBook.aspx.cs" Inherits="BookShop.ViewBook" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-4">
        <h2 class="text-center">Book Details</h2>

        <div class="row mt-4">
            <div class="col-md-6">
                <strong>Title:</strong>
                <asp:Label ID="lblTitle" runat="server" Text=""></asp:Label>
            </div>
            <div class="col-md-6">
                <strong>Author:</strong>
                <asp:Label ID="lblAuthor" runat="server" Text=""></asp:Label>
            </div>
        </div>

        <div class="row mt-2">
            <div class="col-md-6">
                <strong>Published Date:</strong>
                <asp:Label ID="lblPublishedDate" runat="server" Text=""></asp:Label>
            </div>
            <div class="col-md-6">
                <strong>Price:</strong>
                <asp:Label ID="lblPrice" runat="server" Text=""></asp:Label>
            </div>
        </div>

        <div class="row mt-2">
            <div class="col-md-6">
                <strong>Category:</strong>
                <asp:Label ID="lblCategory" runat="server" Text=""></asp:Label>
            </div>
        </div>

        <div class="mt-4">
            <asp:Button ID="btnBack" runat="server" Text="Back to Books" CssClass="btn btn-secondary" OnClick="btnBack_Click" />
        </div>
    </div>
</asp:Content>

