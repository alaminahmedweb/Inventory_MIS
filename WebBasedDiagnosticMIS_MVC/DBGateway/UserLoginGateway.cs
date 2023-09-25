using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using WebBasedDiagnosticMIS_MVC.Models;

namespace WebBasedDiagnosticMIS_MVC.DBGateway
{
    public class UserLoginGateway
    {
        private string message;

        public string IsValidUser(UserLogin userLogin)
        {
            try
            {
                int userId = 0;
                string ConnectionString = ConfigurationManager.ConnectionStrings["InventoryConnStr"].ConnectionString;
                using (SqlConnection con = new SqlConnection(ConnectionString))
                {
                    string query = @"SELECT COUNT(UserName) AS UserId FROM Password WHERE UserName=@UserName AND Password=@Password ";

                    SqlCommand cmd = new SqlCommand(query, con);

                    cmd.Parameters.AddWithValue("@Username", userLogin.UserName);
                    cmd.Parameters.AddWithValue("@Password", userLogin.Password);

                    con.Open();

                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        userId = (int)reader["UserId"];
                    }
                    con.Close();
                }

                switch (userId)
                {
                    case 0:
                        message = "Username and/or password is incorrect.";
                        break;
                    default:
                        message = "Successfully Loged in";
                        //Session.Clear();
                        //Session["UserName"] = username.Text;
                        // Session["ChamberName"] = chamberDropdownList.SelectedItem;
                        // Session["ChamberId"] = chamberDropdownList.SelectedValue;
                        //Response.Redirect("~/UI/Index.aspx");
                        // Response.Redirect("~/UI/BranchLogin.aspx?UserName=" + username.Text + "");
                        break;
                }
            }
            catch (Exception exception)
            {
                message = exception.Message;
            }
            return message;
        }
    }
}