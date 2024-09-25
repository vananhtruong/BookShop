using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BookShop
{
    public partial class Main : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindCategories();
                BindProducts();
            }
        }

        // Method to bind categories to dropdown
        private void BindCategories()
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnectionString"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT CategoryID, CategoryName FROM BookCategory WHERE IsDeleted = 0", conn);
                conn.Open();
                ddlCategory.DataSource = cmd.ExecuteReader();
                ddlCategory.DataTextField = "CategoryName";
                ddlCategory.DataValueField = "CategoryID";
                ddlCategory.DataBind();
            }
            ddlCategory.Items.Insert(0, new ListItem("All Categories", "0"));
        }

        // Method to bind products to GridView with search and filter
        private void BindProducts(string searchKeyword = "", int categoryId = 0)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnectionString"].ConnectionString))
            {
                string query = "SELECT b.BookID, b.Title, b.Author, b.Price, c.CategoryName " +
                               "FROM Book b " +
                               "INNER JOIN BookCategory c ON b.CategoryID = c.CategoryID " +
                               "WHERE b.IsDeleted = 0 AND " +
                               "(@CategoryID = 0 OR b.CategoryID = @CategoryID) " +
                               "AND (b.Title LIKE @SearchKeyword OR b.Author LIKE @SearchKeyword) " +
                               "ORDER BY b.Title";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@CategoryID", categoryId);
                cmd.Parameters.AddWithValue("@SearchKeyword", "%" + searchKeyword + "%");
                conn.Open();

                gvProducts.DataSource = cmd.ExecuteReader();
                gvProducts.DataBind();
            }
        }

        // Handle search button click
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string searchKeyword = txtSearch.Text.Trim();
            int categoryId = int.Parse(ddlCategory.SelectedValue);
            BindProducts(searchKeyword, categoryId);
        }

        // Handle category filter change
        protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            string searchKeyword = txtSearch.Text.Trim();
            int categoryId = int.Parse(ddlCategory.SelectedValue);
            BindProducts(searchKeyword, categoryId);
        }

        // Handle Detail button click
        protected void btnDetail_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int bookId = Convert.ToInt32(btn.CommandArgument);
            // Redirect to detail page or load detail information
            Response.Redirect("BookDetail.aspx?BookID=" + bookId);
        }

        // Handle Add to Cart button click
        protected void btnAddToCart_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;

            // Tách dữ liệu từ CommandArgument (BookID, Title, Price)
            string[] commandArgs = btn.CommandArgument.Split(';');
            int bookId = Convert.ToInt32(commandArgs[0]);
            string title = commandArgs[1];
            decimal price = Convert.ToDecimal(commandArgs[2]);

            // Lấy giỏ hàng từ session hoặc khởi tạo mới nếu chưa có
            DataTable cart = Session["Cart"] as DataTable;
            if (cart == null)
            {
                cart = new DataTable();
                cart.Columns.Add("BookID", typeof(int));
                cart.Columns.Add("Title", typeof(string));
                cart.Columns.Add("Price", typeof(decimal));
                cart.Columns.Add("Quantity", typeof(int));
            }

            // Thêm sách vào giỏ hàng hoặc tăng số lượng nếu đã tồn tại
            DataRow[] existingRows = cart.Select("BookID = " + bookId);
            if (existingRows.Length > 0)
            {
                existingRows[0]["Quantity"] = (int)existingRows[0]["Quantity"] + 1;
            }
            else
            {
                DataRow row = cart.NewRow();
                row["BookID"] = bookId;
                row["Title"] = title;
                row["Price"] = price;
                row["Quantity"] = 1;
                cart.Rows.Add(row);
            }

            // Lưu giỏ hàng vào session
            Session["Cart"] = cart;
        }

    }
}
