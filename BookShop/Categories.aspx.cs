using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BookShop
{
    public partial class Categories : System.Web.UI.Page
    {
        string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnectionString"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindCategoriesGrid();
            }
        }

        private void BindCategoriesGrid(string searchQuery = null)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT CategoryID, CategoryName FROM BookCategory WHERE IsDeleted = 0";

                if (!string.IsNullOrEmpty(searchQuery))
                {
                    query += " AND CategoryName LIKE '%' + @SearchQuery + '%'";
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
                        gvCategories.DataSource = dt;
                        gvCategories.DataBind();
                    }
                }
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string searchQuery = txtSearch.Text;
            BindCategoriesGrid(searchQuery);
        }

        protected void btnClearSearch_Click(object sender, EventArgs e)
        {
            txtSearch.Text = string.Empty;
            BindCategoriesGrid();
        }

        protected void gvCategories_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int categoryId = Convert.ToInt32(e.CommandArgument);

            if (e.CommandName == "View")
            {
                Response.Redirect($"ViewCategory.aspx?CategoryID={categoryId}");
            }
            else if (e.CommandName == "Edit")
            {
                Response.Redirect($"EditCategory.aspx?CategoryID={categoryId}");
            }
            else if (e.CommandName == "Delete")
            {
                DeleteCategory(categoryId);
                BindCategoriesGrid();
            }
        }

        private void DeleteCategory(int categoryId)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "UPDATE BookCategory SET IsDeleted = 1 WHERE CategoryID = @CategoryID";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@CategoryID", categoryId);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
        }

        protected void btnCreate_Click(object sender, EventArgs e)
        {
            Response.Redirect("CreateCategory.aspx");
        }

        protected void gvCategories_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvCategories.PageIndex = e.NewPageIndex;
            BindCategoriesGrid();
        }
    }
}
