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
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text;
            string password = txtPassword.Text;

            // Connection string from Web.config
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnectionString"].ConnectionString;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                string query = "SELECT * FROM AppUser WHERE Email = @Email AND PasswordHash = @PasswordHash AND IsDeleted = 0";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@PasswordHash", password); // Hash password in real applications!

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    // Successful login
                    Session["UserEmail"] = email;
                    Response.Redirect("Home.aspx"); // Redirect to homepage or dashboard
                }
                else
                {
                    // Invalid login
                    lblMessage.Text = "Invalid email or password.";
                    lblMessage.Visible = true;
                }
                con.Close();
            }
        }
        protected void btnCreateAccount_Click(object sender, EventArgs e)
        {
            Response.Redirect("CreateAccount.aspx");
        }
    }
}