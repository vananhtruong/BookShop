<%@ Page Title="Manage Categories" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Categories.aspx.cs" Inherits="BookShop.Categories" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div>
        <h2>Manage Categories</h2>

        <div class="form-group">
            <asp:Label ID="lblSearch" runat="server" Text="Search by Category Name: "></asp:Label>
            <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control"></asp:TextBox>
            <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" CssClass="btn btn-primary" />
            <asp:Button ID="btnClearSearch" runat="server" Text="Clear" OnClick="btnClearSearch_Click" CssClass="btn btn-secondary" />
        </div>

        <asp:Button ID="btnCreate" runat="server" Text="Create New Category" OnClick="btnCreate_Click" CssClass="btn btn-success" />

        <asp:GridView ID="gvCategories" runat="server" AutoGenerateColumns="False" OnRowCommand="gvCategories_RowCommand" 
            DataKeyNames="CategoryID" CssClass="table table-striped" ShowHeaderWhenEmpty="True" 
            EmptyDataText="No categories found." AllowPaging="true" PageSize="10" 
            OnPageIndexChanging="gvCategories_PageIndexChanging" PagerStyle-CssClass="pagination justify-content-center">
            <Columns>
                <asp:BoundField DataField="CategoryID" HeaderText="Category ID" ReadOnly="True" Visible="False" />
                <asp:BoundField DataField="CategoryName" HeaderText="Category Name" />
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:Button ID="btnView" runat="server" Text="View" CommandName="View" CommandArgument='<%# Eval("CategoryID") %>' CssClass="btn btn-info" />
                        <asp:Button ID="btnEdit" runat="server" Text="Edit" CommandName="Edit" CommandArgument='<%# Eval("CategoryID") %>' CssClass="btn btn-warning" />
                        <asp:Button ID="btnDelete" runat="server" Text="Delete" CommandName="Delete" CommandArgument='<%# Eval("CategoryID") %>' CssClass="btn btn-danger" OnClientClick="return confirm('Are you sure you want to delete this category?');" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>

</asp:Content>
