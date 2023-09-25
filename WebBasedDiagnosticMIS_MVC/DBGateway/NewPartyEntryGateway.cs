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
    public class NewPartyEntryGateway
    {
        private string connectionString =
           WebConfigurationManager.ConnectionStrings["InventoryConnStr"].ConnectionString;
        private string message;


        public string Save(NewPartyEntry newPartyEntry)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction();
                try
                {
                    string query = "INSERT INTO PartyInfo(PartyId, PartyName, Address, ContactNo,Valid) " +
                            " VALUES " +
                            " (@PartyId, @PartyName, @Address, @ContactNo,1)";

                    SqlCommand command = new SqlCommand(query, connection, transaction);

                    command.Parameters.Add("PartyId", SqlDbType.VarChar);
                    command.Parameters["PartyId"].Value = newPartyEntry.PartyId;

                    command.Parameters.Add("PartyName", SqlDbType.VarChar);
                    command.Parameters["PartyName"].Value = newPartyEntry.PartyName;

                    command.Parameters.Add("Address", SqlDbType.VarChar);
                    command.Parameters["Address"].Value = newPartyEntry.PartyAddress;

                    command.Parameters.Add("ContactNo", SqlDbType.VarChar);
                    command.Parameters["ContactNo"].Value = newPartyEntry.ContactNo;


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


        public List<NewPartyEntry> GetSupplierList(string param)
        {
            var lists = new List<NewPartyEntry>();
            string cond = "";
            if (param != "")
            {
                cond = "AND PartyId='" + param + "'";
            }
            string query = @"SELECT PartyId AS ID, PartyName AS Name,ContactNo AS MobileNo, Address " + 
                           " FROM PartyInfo "+
                           " WHERE Valid=1 " + cond + " "+
                           " ORDER BY PartyName";
            SqlConnection Con = new SqlConnection(connectionString);
            Con.Open();
            var cmd = new SqlCommand(query, Con);
            SqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                lists.Add(new NewPartyEntry
                {
                    PartyId = rdr["Id"].ToString(),
                    PartyName = rdr["Name"].ToString(),
                    PartyAddress = rdr["Address"].ToString(),
                    ContactNo = rdr["MobileNo"].ToString(),

                });
            }

            rdr.Close();
            Con.Close();
            return lists;
        }
    }
}