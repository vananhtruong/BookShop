using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BookShop
{
    public partial class Books : System.Web.UI.Page
    {
        // Database connection string
        string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnectionString"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindBooksGrid();
            }
        }

        // Bind data to the GridView with optional search query
        private void BindBooksGrid(string searchQuery = null)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT BookID, Title, Author, PublishedDate, Price FROM Book WHERE IsDeleted = 0";

                if (!string.IsNullOrEmpty(searchQuery))
                {
                    query += " AND Title LIKE '%' + @SearchQuery + '%'";
                }

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    if (!string.IsNullOrEmpty(searchQuery))
                    {
                        cmd.Parameters.AddWithValue("@SearchQuery", searchQuery);
                    }

                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        gvBooks.DataSource = dt;
                        gvBooks.DataBind();
                    }
                }
            }
        }

        // Search button click event
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string searchQuery = txtSearch.Text;
            BindBooksGrid(searchQuery);
        }

        // Clear search button click event
        protected void btnClearSearch_Click(object sender, EventArgs e)
        {
            txtSearch.Text = string.Empty;
            BindBooksGrid();
        }

        // Handle GridView row commands (View, Edit, Delete)
        protected void gvBooks_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int bookID = Convert.ToInt32(e.CommandArgument);

            if (e.CommandName == "View")
            {
                Response.Redirect($"ViewBook.aspx?BookID={bookID}");
            }
            else if (e.CommandName == "Edit")
            {
                Response.Redirect($"EditBook.aspx?BookID={bookID}");
            }
            else if (e.CommandName == "Delete")
            {
                DeleteBook(bookID);
                BindBooksGrid();
            }
        }

        // Delete book logic
        private void DeleteBook(int bookID)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "UPDATE Book SET IsDeleted = 1 WHERE BookID = @BookID";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@BookID", bookID);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
        }

        // Handle pagination in GridView
        protected void gvBooks_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvBooks.PageIndex = e.NewPageIndex;
            BindBooksGrid();
        }

        // Create button click event
        protected void btnCreate_Click(object sender, EventArgs e)
        {
            Response.Redirect("CreateBook.aspx");
        }
    }
}
