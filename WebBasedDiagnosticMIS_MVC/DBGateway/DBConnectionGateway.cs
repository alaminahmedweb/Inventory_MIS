using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Configuration;
//using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.CrystalReports.Engine;
using WebBasedDiagnosticMIS_MVC.Report.Dataset;


namespace WebBasedDiagnosticMIS_MVC.DBGateway
{
    public class DBConnectionGateway
    {

        public SqlConnection Con = new SqlConnection(WebConfigurationManager.ConnectionStrings["InventoryConnStr"].ConnectionString);
        public string DeleteInsert(string id)
        {
            try
            {
                Con.Open();
                string autoId = id;
                var command = new SqlCommand(autoId, Con);
                return command.ExecuteNonQuery().ToString();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                if (Con.State == ConnectionState.Open)
                {
                    Con.Close();
                }
            }
        }
        public bool FncSeekRecordNew(string lcTableName, string lcCondition)
        {
            string query = "";
            if (lcCondition != "")
            {
                query = "Select * from " + lcTableName + " where " + lcCondition + "";
            }
            else
            {
                query = "Select * from " + lcTableName + "";
            }
            Con.Open();
            var cmd = new SqlCommand(query, Con);
            var aReader = cmd.ExecuteReader();
            bool lnTrueFlase = aReader.HasRows;
            Con.Close();
            return lnTrueFlase;
        }
        public string ReturnFieldValue(string lcTableName, string lcCondition, string lcFieldName)
        {
            string query = "", result = "";
            if (lcCondition != "")
            {
                query = "Select " + lcFieldName + " as Description from " + lcTableName + " where " + lcCondition + "";
            }
            else
            {
                query = "Select " + lcFieldName + " as Description from " + lcTableName + "";
            }
            Con.Open();
            var aCommand = new SqlCommand(query, Con);
            SqlDataReader aReader = aCommand.ExecuteReader();

            while (aReader.Read())
            {
                result = aReader["Description"].ToString();
            }
            aReader.Close();
            Con.Close();
            return result;
        }
        public DataTable ConvertListDataTable<T>(List<T> items)
        {
            var dataTable = new DataTable(typeof(T).Name);
            PropertyInfo[] props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in props)
            {
                dataTable.Columns.Add(prop.Name);
            }
            foreach (T item in items)
            {
                var values = new object[props.Length];
                for (int i = 0; i < props.Length; i++)
                {
                    values[i] = props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }
            return dataTable;
        }
        public int GetMaxId(string tableName, string fieldName)
        {
            int slNo = 0;
            string sql = @"SELECT  Isnull(MAX(" + fieldName + "),0)+1 AS TrNo  FROM " + tableName + " ";
            Con.Open();
            var aCommand = new SqlCommand(sql, Con);
            SqlDataReader aReader = aCommand.ExecuteReader();
            while (aReader.Read())
            {
                slNo = Convert.ToInt32(aReader["TrNo"].ToString());
            }
            Con.Close();
            return slNo;
        }
        public string GetInvoiceNo(int stockType, SqlTransaction trans)
        {
            string lcsql = @"Exec SP_GET_INVOICENO " + stockType + "";
            // Con.Open();
            var aCommand = new SqlCommand(lcsql, Con, trans);
            SqlDataReader aReader = aCommand.ExecuteReader();
            while (aReader.Read())
            {
                lcsql = aReader["InvNo"].ToString();
            }
            // Con.Close();
            aReader.Close();
            return lcsql;
        }
        public string GetTrNo(string fieldName, string tableName, SqlTransaction trans)
        {
            string lcsql = @"SELECT  RIGHT('00' + Convert(varchar,YEAR(GETDATE())), 2) + RIGHT('00' + Convert(varchar,MONTH(Getdate())), 2) + RIGHT('0000'+ Convert(varchar,ISNULL(Max(Convert(integer, RIGHT(" + fieldName + ", 6))),0)+ 1), 4) AS TrNo FROM " + tableName + "";
            // Con.Open();
            var aCommand = new SqlCommand(lcsql, Con, trans);
            SqlDataReader aReader = aCommand.ExecuteReader();
            while (aReader.Read())
            {
                lcsql = aReader["TrNo"].ToString();
            }
            // Con.Close();
            aReader.Close();
            return lcsql;
        }
        public DataTable GetDataFromProc(DataTable table, string lcparameter, string lcProcedureName, string dataReadBy)
        {
            string lcStrSql = "";
            if (dataReadBy != "V")
            {
                lcStrSql = "" + lcProcedureName + " " + lcparameter;
            }
            else
            {
                lcStrSql = "SELECT * FROM " + lcProcedureName + " " + lcparameter;
            }
            var cmd = new SqlCommand(lcStrSql, Con);
            var objAdapter = new SqlDataAdapter(cmd);
            objAdapter.Fill(table);
            return table;
        }
        public void AddParameters(Hashtable hParameter, String lcComName, String lcComAddress, String lcTitle, String lcDateRange)
        {
            hParameter.Add("lcComName", lcComName);
            hParameter.Add("lcComAddress", lcComAddress);
            hParameter.Add("lcTitle", lcTitle);
            hParameter.Add("lcDateRange", lcDateRange);
        }

      
       
    }
}