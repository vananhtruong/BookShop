using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
using System.Web.UI;

namespace BookShop
{
    public partial class CreateAccount : Page
    {
        // Database connection string
        string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnectionString"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        // Event handler for Create Account button click
        protected void btnCreateAccount_Click(object sender, EventArgs e)
        {
            // Hash the password using SHA256
            string passwordHash = HashPassword(txtPassword.Text);

            // Insert user data into the database
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO AppUser (FullName, Email, PasswordHash, Role, IsDeleted) VALUES (@FullName, @Email, @PasswordHash, @Role, 0)";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@FullName", txtFullName.Text);
                    cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
                    cmd.Parameters.AddWithValue("@PasswordHash", passwordHash);
                    cmd.Parameters.AddWithValue("@Role", "User");

                    con.Open();
                    int result = cmd.ExecuteNonQuery();
                    con.Close();

                    if (result > 0)
                    {
                        lblMessage.Text = "Account created successfully!";
                        lblMessage.CssClass = "text-success";
                    }
                    else
                    {
                        lblMessage.Text = "Error creating account.";
                        lblMessage.CssClass = "text-danger";
                    }

                    lblMessage.Visible = true;
                }
            }
        }

        // Method to hash the password using SHA256
        private string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();

                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }

                return builder.ToString();
            }
        }
    }
}
