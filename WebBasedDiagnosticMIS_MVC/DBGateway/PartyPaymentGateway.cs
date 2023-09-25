using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using WebBasedDiagnosticMIS_MVC.Models;

namespace WebBasedDiagnosticMIS_MVC.DBGateway
{
    public class PartyPaymentGateway:DBConnectionGateway
    {
        private SqlTransaction _trans;

        public string Save(PartyPayment aPartyPayment)
        {
            try
            {
                Con.Open();
                //Thread.Sleep(5);

                _trans = Con.BeginTransaction();

                string slipNo = "GPN" + GetTrNo("RefNo", "PartyLedger", _trans);

                const string query = @"INSERT INTO PartyLedger (RefNo, RefDate, Particulars, PurchaseAmt, " +
                    " PaymentAmt, Less, ReturnAmt, CompanyID, UserName, Valid, PINo, PIDate,ReceiptNo,ReceiptDate,Remarks) " +
                    " VALUES (@RefNo, @RefDate, @Particulars, @PurchaseAmt,  @PaymentAmt, @Less, @ReturnAmt," +
                    " @CompanyID, @UserName, @Valid, @PINo, @PIDate,@ReceiptNo,@ReceiptDate,@Remarks)";

                var cmd = new SqlCommand(query, Con, _trans);

                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@RefNo", slipNo);
                cmd.Parameters.AddWithValue("@RefDate", DateTime.Now.ToString("yyy-MM-dd"));
                cmd.Parameters.AddWithValue("@PINo", slipNo);
                cmd.Parameters.AddWithValue("@PIDate", DateTime.Now.ToString("yyy-MM-dd"));
                cmd.Parameters.AddWithValue("@ReceiptNo", slipNo);
                cmd.Parameters.AddWithValue("@ReceiptDate", DateTime.Now.ToString("yyy-MM-dd"));
                cmd.Parameters.AddWithValue("@Particulars", "Payment");
                cmd.Parameters.AddWithValue("@CompanyID", aPartyPayment.PartyId);
                cmd.Parameters.AddWithValue("@PurchaseAmt", "0");
                cmd.Parameters.AddWithValue("@PaymentAmt", aPartyPayment.PaymentAmt);
                cmd.Parameters.AddWithValue("@Less", aPartyPayment.LessAmt);
                cmd.Parameters.AddWithValue("@ReturnAmt", "0");
                cmd.Parameters.AddWithValue("@Remarks", aPartyPayment.Remarks ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@UserName", "Admin");
                cmd.Parameters.AddWithValue("@Valid", "1");

                cmd.ExecuteNonQuery();



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


        public List<PartyPayment> GetSupplierList(string param)
        {
            var lists = new List<PartyPayment>();
            string cond = "";
            if (param != "")
            {
                cond = "AND PartyId='" + param + "'";
            }
            string query = @"SELECT PartyId AS ID, PartyName AS Name,ContactNo AS MobileNo, Address,Due " +
                           " FROM VW_PartyWiseDue " +
                           " WHERE Due>0 " + cond + " " +
                           " ORDER BY PartyName";
           // SqlConnection Con = new SqlConnection(connectionString);
            Con.Open();
            var cmd = new SqlCommand(query, Con);
            SqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                lists.Add(new PartyPayment
                {
                    PartyId = rdr["Id"].ToString(),
                    PartyName = rdr["Name"].ToString(),
                    PartyAddress = rdr["Address"].ToString(),
                    ContactNo = rdr["MobileNo"].ToString(),
                    TotalDueAmt = Convert.ToDouble(rdr["Due"].ToString())
                });
            }

            rdr.Close();
            Con.Close();
            return lists;
        }
    }
}