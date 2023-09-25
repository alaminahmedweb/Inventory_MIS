using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Configuration;
using Microsoft.SqlServer.Server;
using WebBasedDiagnosticMIS_MVC.Models;

namespace WebBasedDiagnosticMIS_MVC.DBGateway
{
    public class StockReceiveGateway:DBConnectionGateway
    {
        private SqlTransaction _trans;
       
        public string Save(List<StockReceive> aStockReceiveNewItemModel)
        {
            try
            {
                Con.Open();
                //Thread.Sleep(5);
                
                _trans = Con.BeginTransaction();
                
                string slipNo ="GRN"+ GetTrNo("SlipNo", "StockLedger", _trans);

                const string query = @"INSERT INTO PartyLedger (RefNo, RefDate, Particulars, PurchaseAmt, " +
                    " PaymentAmt, Less, ReturnAmt, CompanyID, UserName, Valid, PINo, PIDate,ReceiptNo,ReceiptDate,Remarks) " +
                    " VALUES (@RefNo, @RefDate, @Particulars, @PurchaseAmt,  @PaymentAmt, @Less, @ReturnAmt,"+
                    " @CompanyID, @UserName, @Valid, @PINo, @PIDate,@ReceiptNo,@ReceiptDate,@Remarks)";
                
                var cmd = new SqlCommand(query, Con, _trans);

                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@RefNo", slipNo);
                cmd.Parameters.AddWithValue("@RefDate", DateTime.Now.ToString("yyy-MM-dd"));
                cmd.Parameters.AddWithValue("@PINo", slipNo);
                cmd.Parameters.AddWithValue("@PIDate", DateTime.Now.ToString("yyy-MM-dd"));
                cmd.Parameters.AddWithValue("@ReceiptNo", aStockReceiveNewItemModel.ElementAt(0).ReceiptNo ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@ReceiptDate", aStockReceiveNewItemModel.ElementAt(0).ReceiptDate.ToString("yyyy-MM-dd"));
                cmd.Parameters.AddWithValue("@Particulars", "Purchase"); 
                cmd.Parameters.AddWithValue("@CompanyID", aStockReceiveNewItemModel.ElementAt(0).PartyId);
                cmd.Parameters.AddWithValue("@PurchaseAmt", aStockReceiveNewItemModel.ElementAt(0).InvoiceValue);
                cmd.Parameters.AddWithValue("@PaymentAmt","0");
                cmd.Parameters.AddWithValue("@Less", "0");
                cmd.Parameters.AddWithValue("@ReturnAmt", "0");
                cmd.Parameters.AddWithValue("@Remarks", aStockReceiveNewItemModel.ElementAt(0).Remarks ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@UserName", "Admin");
                cmd.Parameters.AddWithValue("@Valid", "1");

                cmd.ExecuteNonQuery();


                aStockReceiveNewItemModel.ForEach(z => z.SlipNo = slipNo);
                aStockReceiveNewItemModel.ForEach(z => z.TrDate = DateTime.Now.Date);
                aStockReceiveNewItemModel.ForEach(z => z.StockStatus = 1);
                aStockReceiveNewItemModel.ForEach(z => z.UserName = "Admin");
                aStockReceiveNewItemModel.ForEach(z => z.EntryTime = DateTime.Now.ToLocalTime().ToString());
                aStockReceiveNewItemModel.ForEach(z => z.Valid = 1);
                string query2;
                foreach (var data in aStockReceiveNewItemModel)
                {
                    query2 = @"INSERT INTO StockLedger (SlipNo, ProductId, ProductName, Unit, UnitPrice, StockIn, " + 
                        " StockOut, CReturn, TrDate, StockStatus, valid, UserName, EntryTime, Remarks, "+
                        " ReceiptNo, ReceiptDate, CompanyId) " +
                        " VALUES ('" + data.SlipNo + "', '" + data.ProductId + "',  '" + data.ProductName + "',"+
                        "  '" + data.Unit + "',   '" + data.UnitPrice + "',  '" + data.Quantity + "',0," +
                        " 0, '" + data.TrDate.ToString("yyy-MM-dd") + "',  '" + data.StockStatus + "',  '" + data.Valid + "',  '" + data.UserName + "'," +
                        " '" + data.EntryTime + "', '" + data.Remarks + "', '" + data.ReceiptNo + "','" + data.ReceiptDate.ToString("yyy-MM-dd") + "', '" + data.PartyId + "')";
                    cmd = new SqlCommand(query2, Con, _trans);
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
    }
}