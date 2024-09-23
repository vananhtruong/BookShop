<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Books.aspx.cs" Inherits="BookShop.Books" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-4">
        <h2 class="text-center">Manage Books</h2>

        <div class="form-row mb-3">
            <div class="col-md-6">
                <asp:Label ID="lblSearch" runat="server" Text="Search by Title: " CssClass="form-label"></asp:Label>
                <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control" placeholder="Enter book title"></asp:TextBox>
            </div>
            <div class="col-md-6 d-flex align-items-end">
                <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" CssClass="btn btn-primary mr-2" />
                <asp:Button ID="btnClearSearch" runat="server" Text="Clear" OnClick="btnClearSearch_Click" CssClass="btn btn-secondary" />
            </div>
        </div>

        <div class="mb-3">
            <asp:Button ID="btnCreate" runat="server" Text="Create New Book" OnClick="btnCreate_Click" CssClass="btn btn-success" />
        </div>

        <asp:GridView ID="gvBooks" runat="server" AutoGenerateColumns="False" OnRowCommand="gvBooks_RowCommand"
            DataKeyNames="BookID" CssClass="table table-striped  table-hover" ShowHeaderWhenEmpty="True"
            EmptyDataText="No books found." AllowPaging="true" PageSize="2" OnPageIndexChanging="gvBooks_PageIndexChanging">

            <Columns>
                <asp:BoundField DataField="BookID" HeaderText="Book ID" ReadOnly="True" Visible="False" />
                <asp:BoundField DataField="Title" HeaderText="Title" />
                <asp:BoundField DataField="Author" HeaderText="Author" />
                <asp:BoundField DataField="PublishedDate" HeaderText="Published Date" DataFormatString="{0:yyyy-MM-dd}" />
                <asp:BoundField DataField="Price" HeaderText="Price" DataFormatString="{0:C}" />
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:Button ID="btnView" runat="server" Text="View" CommandName="View" CommandArgument='<%# Eval("BookID") %>' CssClass="btn btn-info" />
                        <asp:Button ID="btnEdit" runat="server" Text="Edit" CommandName="Edit" CommandArgument='<%# Eval("BookID") %>' CssClass="btn btn-warning" />
                        <asp:Button ID="btnDelete" runat="server" Text="Delete" CommandName="Delete" CommandArgument='<%# Eval("BookID") %>' CssClass="btn btn-danger" OnClientClick="return confirm('Are you sure you want to delete this book?');" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>

    </div>
</asp:Content>
