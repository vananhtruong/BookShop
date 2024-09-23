using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BookShop
{
    public partial class EditBook : System.Web.UI.Page
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
                    LoadCategories();
                    LoadBookDetails(bookID);
                }
            }
        }

        // Load book details into the form fields
        private void LoadBookDetails(int bookID)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = @"SELECT Title, Author, PublishedDate, Price, CategoryID 
                                 FROM Book 
                                 WHERE BookID = @BookID AND IsDeleted = 0";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@BookID", bookID);
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            txtTitle.Text = reader["Title"].ToString();
                            txtAuthor.Text = reader["Author"].ToString();
                            txtPublishedDate.Text = Convert.ToDateTime(reader["PublishedDate"]).ToString("yyyy-MM-dd");
                            txtPrice.Text = Convert.ToDecimal(reader["Price"]).ToString("F2");
                            ddlCategory.SelectedValue = reader["CategoryID"].ToString();  // Select category in dropdown
                        }
                    }
                    con.Close();
                }
            }
        }

        // Load categories into the dropdown list
        private void LoadCategories()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT CategoryID, CategoryName FROM BookCategory WHERE IsDeleted = 0";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        ddlCategory.DataSource = reader;
                        ddlCategory.DataTextField = "CategoryName";
                        ddlCategory.DataValueField = "CategoryID";
                        ddlCategory.DataBind();
                    }
                    con.Close();
                }
            }
            ddlCategory.Items.Insert(0, new ListItem("-- Select Category --", ""));  // Add a default option
        }

        // Save changes to the book
        protected void btnSave_Click(object sender, EventArgs e)
        {
            int bookID = Convert.ToInt32(Request.QueryString["BookID"]);

            if (bookID > 0)
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string query = @"UPDATE Book 
                                     SET Title = @Title, Author = @Author, PublishedDate = @PublishedDate, 
                                         Price = @Price, CategoryID = @CategoryID 
                                     WHERE BookID = @BookID";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@Title", txtTitle.Text);
                        cmd.Parameters.AddWithValue("@Author", txtAuthor.Text);
                        cmd.Parameters.AddWithValue("@PublishedDate", Convert.ToDateTime(txtPublishedDate.Text));
                        cmd.Parameters.AddWithValue("@Price", Convert.ToDecimal(txtPrice.Text));
                        cmd.Parameters.AddWithValue("@CategoryID", ddlCategory.SelectedValue);
                        cmd.Parameters.AddWithValue("@BookID", bookID);

                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                }

                // Redirect back to the Books page after saving
                Response.Redirect("Books.aspx");
            }
        }

        // Cancel button click event
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("Books.aspx");
        }
    }
}
