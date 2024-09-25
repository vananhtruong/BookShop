<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Main.aspx.cs" Inherits="BookShop.Main" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <div class="row">
            <div class="col-md-3">
                <div class="form-group">
                    <label for="txtSearch">Search by Title or Author:</label>
                    <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control" />
                </div>

                <div class="form-group">
                    <label for="ddlCategory">Filter by Category:</label>
                    <asp:DropDownList ID="ddlCategory" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged">
                        <asp:ListItem Text="All Categories" Value="0"></asp:ListItem>
                    </asp:DropDownList>
                </div>

                <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-primary" Text="Search" OnClick="btnSearch_Click" />
            </div>

            <div class="col-md-9">
                <asp:GridView ID="gvProducts" runat="server" CssClass="table table-striped" AutoGenerateColumns="False">
                    <Columns>
                        <asp:BoundField DataField="Title" HeaderText="Book Title" />
                        <asp:BoundField DataField="Author" HeaderText="Author" />
                        <asp:BoundField DataField="CategoryName" HeaderText="Category" />
                        <asp:BoundField DataField="Price" HeaderText="Price" DataFormatString="{0:C}" />

                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:Button ID="btnDetail" runat="server" Text="Detail" CssClass="btn btn-info" CommandArgument='<%# Eval("BookID") %>' OnClick="btnDetail_Click" />
                                <asp:Button ID="btnAddToCart" runat="server" Text="Add to Cart" CssClass="btn btn-success"
                                    CommandArgument='<%# Eval("BookID") + ";" + Eval("Title") + ";" + Eval("Price") %>'
                                    OnClick="btnAddToCart_Click" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>

            </div>
        </div>
    </div>
</asp:Content>
