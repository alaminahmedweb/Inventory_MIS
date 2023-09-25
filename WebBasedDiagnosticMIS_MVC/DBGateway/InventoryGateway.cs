using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using WebBasedDiagnosticMIS_MVC.Models;

namespace WebBasedDiagnosticMIS_MVC.DBGateway
{
    public class InventoryGateway
    {
        private string connectionString =
           WebConfigurationManager.ConnectionStrings["InventoryConnStr"].ConnectionString;
        private string message;


        public string Save(ProductList productList)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction();
                try
                {
                    string query = "INSERT INTO SalesProductList(ProductID, ProductName, ProductCategory, UnitPrice, Unit, Valid,  " +
                            " ReminderStock,  Type, AssetType) " +
                            " VALUES " +
                            " (@ProductID, @ProductName, @ProductCategory, @UnitPrice, @Unit, 1,  " +
                            " @ReminderStock,  @Type, @AssetType)";

                    SqlCommand command = new SqlCommand(query, connection, transaction);

                    command.Parameters.Add("ProductID", SqlDbType.VarChar);
                    command.Parameters["ProductID"].Value = productList.ProductId;

                    command.Parameters.Add("ProductName", SqlDbType.VarChar);
                    command.Parameters["ProductName"].Value = productList.ProductName;

                    command.Parameters.Add("ProductCategory", SqlDbType.VarChar);
                    command.Parameters["ProductCategory"].Value = productList.ProductCategory;

                    command.Parameters.Add("UnitPrice", SqlDbType.VarChar);
                    command.Parameters["UnitPrice"].Value = productList.UnitPrice;

                    command.Parameters.Add("Unit", SqlDbType.VarChar);
                    command.Parameters["Unit"].Value = productList.Unit;

                    command.Parameters.Add("ReminderStock", SqlDbType.VarChar);
                    command.Parameters["ReminderStock"].Value = productList.ReminderStock;

                    command.Parameters.Add("Type", SqlDbType.VarChar);
                    command.Parameters["Type"].Value = productList.Type;

                    command.Parameters.Add("AssetType", SqlDbType.VarChar);
                    command.Parameters["AssetType"].Value = productList.AssetType;

                    int rowAffected = command.ExecuteNonQuery();
                    if (rowAffected > 0)
                    {
                        message = "Saved Successfully";

                    }
                    else
                    {
                        message = "Failed To Save...!!!";
                    }

                    transaction.Commit();
                    return message;
                }
                catch (Exception exception)
                {
                    message = exception.Message;
                    transaction.Rollback();
                    return message;
                }
            }
        }
        public List<ProductCategory> GetProductCategory()
        {
            string sql = "SELECT GroupName FROM GroupInfo ORDER BY GroupName";

            List<Models.ProductCategory> productCategories = new List<Models.ProductCategory>();
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = new SqlCommand(sql, connection);

            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                ProductCategory productCategory = new ProductCategory();

                productCategory.GroupName = reader["GroupName"].ToString();

                productCategories.Add(productCategory);
            }
            return productCategories;
        }

        public List<ProductList> GetProductList()
        {
            string sql = " SELECT ProductID, ProductName " +
                         " FROM SalesProductList "+
                         " ORDER BY ProductName";

            List<Models.ProductList> list = new List<Models.ProductList>();
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = new SqlCommand(sql, connection);

            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                ProductList productList = new ProductList();

                productList.ProductId = reader["ProductID"].ToString();
                productList.ProductName = reader["ProductName"].ToString();

                list.Add(productList);
            }
            return list;
        }

        public List<ProductList> GetProductListById(string productId)
        {
            string sql = " SELECT ProductID, ProductName, UnitPrice, Unit "+
                         " FROM SalesProductList "+
                         " WHERE ProductId='"+ productId +"' "+
                         " ORDER BY ProductID";

            List<Models.ProductList> list = new List<Models.ProductList>();
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = new SqlCommand(sql, connection);

            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                ProductList productList = new ProductList();

                productList.Unit = reader["Unit"].ToString();
                productList.UnitPrice = Convert.ToDouble(reader["UnitPrice"].ToString());


                list.Add(productList);
            }
            return list;
        }

       
    }
}