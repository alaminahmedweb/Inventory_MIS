using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using WebBasedDiagnosticMIS_MVC.Models;

namespace WebBasedDiagnosticMIS_MVC.DBGateway
{
    public class StockIssueGateway:DBConnectionGateway
    {
        private SqlTransaction _trans;

        public string Save(List<StockIssue> aStockIssue)
        {
            try
            {
                Con.Open();
                //Thread.Sleep(5);

                _trans = Con.BeginTransaction();

                string slipNo = "GIN" + GetTrNo("SlipNo", "StockLedger", _trans);


                aStockIssue.ForEach(z => z.SlipNo = slipNo);
                aStockIssue.ForEach(z => z.TrDate = DateTime.Now.Date);
                aStockIssue.ForEach(z => z.StockStatus = 1);
                aStockIssue.ForEach(z => z.UserName = "Admin");
                aStockIssue.ForEach(z => z.EntryTime = DateTime.Now.ToLocalTime().ToString());
                aStockIssue.ForEach(z => z.Valid = 1);
                string query;
                SqlCommand cmd;
                foreach (var data in aStockIssue)
                {
                    query = @"INSERT INTO StockLedger (SlipNo, ProductId, ProductName, Unit, UnitPrice, StockIn, " +
                        " StockOut, CReturn, TrDate, StockStatus, valid, UserName, EntryTime, Remarks, " +
                        " ReceiptNo, ReceiptDate, CompanyId) " +
                        " VALUES ('" + data.SlipNo + "', '" + data.ProductId + "',  '" + data.ProductName + "'," +
                        "  '" + data.Unit + "',   '" + data.UnitPrice + "',  0,'" + data.Quantity + "'," +
                        " 0, '" + data.TrDate.ToString("yyy-MM-dd") + "',  '" + data.StockStatus + "',  '" + data.Valid + "',  '" + data.UserName + "'," +
                        " '" + data.EntryTime + "', '" + data.Remarks + "', '" + data.ReceiptNo + "','" + data.ReceiptDate.ToString("yyy-MM-dd") + "', '" + data.PartyId + "')";
                     cmd = new SqlCommand(query, Con, _trans);
                     cmd.ExecuteNonQuery();
                }


                _trans.Commit();
                Con.Close();
                return "Saved Success";
            }
            catch (Exception ex)
            {
                if (Con.State == ConnectionState.Open)
                {
                    _trans.Rollback();
                    Con.Close();
                }
                //throw;
                return ex.Message;
            }
        }

        public List<ProductList> GetProductList()
        {
            string sql = " SELECT ProductID, ProductName " +
                         " FROM VW_StockLedger " +
                         " WHERE Balance>0" +
                         " ORDER BY ProductName";

            List<Models.ProductList> list = new List<Models.ProductList>();
           // SqlConnection connection = new SqlConnection(Con);
            Con.Open();
            SqlCommand command = new SqlCommand(sql, Con);

            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                ProductList productList = new ProductList();

                productList.ProductId = reader["ProductID"].ToString();
                productList.ProductName = reader["ProductName"].ToString();

                list.Add(productList);
            }
            Con.Close();
            return list;
        }

        public List<ProductList> GetProductListByIdOnlyBalance(string productId)
        {
            string sql = " SELECT ProductID, ProductName, UnitPrice, Unit,Balance " +
                         " FROM VW_StockLedger " +
                         " WHERE ProductId='" + productId + "' " +
                         " ORDER BY ProductName";

            List<Models.ProductList> list = new List<Models.ProductList>();
           // SqlConnection connection = new SqlConnection(connectionString);
            Con.Open();
            SqlCommand command = new SqlCommand(sql, Con);

            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                ProductList productList = new ProductList();

                productList.Unit = reader["Unit"].ToString();
                productList.UnitPrice = Convert.ToDouble(reader["UnitPrice"].ToString());
                productList.Balance = Convert.ToDouble(reader["Balance"].ToString());

                list.Add(productList);
            }
            Con.Close();
            return list;
        }
    }
}