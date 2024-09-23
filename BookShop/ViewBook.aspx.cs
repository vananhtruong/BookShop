using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BookShop
{
    public partial class ViewBook : System.Web.UI.Page
    {
        // Database connection string
        string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnectionString"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Get the BookID from the query string
                int bookID = Convert.ToInt32(Request.QueryString["BookID"]);

                if (bookID > 0)
                {
                    LoadBookDetails(bookID);
                }
            }
        }

        // Load book details including the category
        private void LoadBookDetails(int bookID)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                // Join Book and BookCategory tables to get category name
                string query = @"SELECT b.Title, b.Author, b.PublishedDate, b.Price, c.CategoryName 
                                 FROM Book b
                                 INNER JOIN BookCategory c ON b.CategoryID = c.CategoryID
                                 WHERE b.BookID = @BookID AND b.IsDeleted = 0";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@BookID", bookID);
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            lblTitle.Text = reader["Title"].ToString();
                            lblAuthor.Text = reader["Author"].ToString();
                            lblPublishedDate.Text = Convert.ToDateTime(reader["PublishedDate"]).ToString("yyyy-MM-dd");
                            lblPrice.Text = Convert.ToDecimal(reader["Price"]).ToString("C");
                            lblCategory.Text = reader["CategoryName"].ToString(); // Display category name
                        }
                    }
                    con.Close();
                }
            }
        }

        // Handle the Back button click event
        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("Books.aspx");
        }
    }
}