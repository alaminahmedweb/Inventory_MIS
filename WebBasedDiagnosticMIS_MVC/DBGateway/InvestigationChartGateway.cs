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
    public class InvestigationChartGateway
    {
        private string connectionString =
            WebConfigurationManager.ConnectionStrings["DiagnosticConnStr"].ConnectionString;

        private string message;
        public string Save(InvestigationChart investigationChart)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction();
                try
                {
                    string query = "INSERT INTO InvestigationChart(DeptName, PCode, ShortDesc, Charge, Discount, " +
                        " DiscountStatus, NormalValue, LessAmount,RptType,UserName,DeliveryDuration, SubDeptName, SubSubDeptName) " +
                        " VALUES(@DeptName, @PCode, @ShortDesc, @Charge,@Discount, " +
                        " @DiscountStatus,@NormalValue, @LessAmount,@RptType,@UserName,@DeliveryDuration, @SubDeptName, @SubSubDeptName)";

                    SqlCommand command = new SqlCommand(query, connection, transaction);

                    command.Parameters.Add("DeptName", SqlDbType.VarChar);
                    command.Parameters["DeptName"].Value = investigationChart.Project;

                    command.Parameters.Add("PCode", SqlDbType.VarChar);
                    command.Parameters["PCode"].Value = investigationChart.Code;

                    command.Parameters.Add("ShortDesc", SqlDbType.VarChar);
                    command.Parameters["ShortDesc"].Value = investigationChart.Name;
                    
                    command.Parameters.Add("Discount", SqlDbType.VarChar);
                    command.Parameters["Discount"].Value = investigationChart.RefFee;

                    command.Parameters.Add("DiscountStatus", SqlDbType.VarChar);
                    command.Parameters["DiscountStatus"].Value = investigationChart.RefFeeType;

                    command.Parameters.Add("NormalValue", SqlDbType.VarChar);
                    command.Parameters["NormalValue"].Value = investigationChart.NormalValue;

                    command.Parameters.Add("LessAmount", SqlDbType.VarChar);
                    command.Parameters["LessAmount"].Value = investigationChart.LessFixedAmount;

                    command.Parameters.Add("RptType", SqlDbType.VarChar);
                    command.Parameters["RptType"].Value = investigationChart.ReportFileName;

                    command.Parameters.Add("UserName", SqlDbType.VarChar);
                    command.Parameters["UserName"].Value = "1";

                    command.Parameters.Add("DeliveryDuration", SqlDbType.VarChar);
                    command.Parameters["DeliveryDuration"].Value = investigationChart.ReportDeliveryAfter;

                    command.Parameters.Add("SubDeptName", SqlDbType.VarChar);
                    command.Parameters["SubDeptName"].Value = investigationChart.SubProject;

                    command.Parameters.Add("SubSubDeptName", SqlDbType.VarChar);
                    command.Parameters["SubSubDeptName"].Value = investigationChart.SubProjectDept;

                    command.Parameters.Add("Charge", SqlDbType.VarChar);
                    command.Parameters["Charge"].Value = investigationChart.Price;

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

        public List<InvestigationChart> GetAllInvestigationList()
        {
            string sql = "SELECT PCode,ShortDesc,Charge,SubSubDeptName FROM InvestigationChart ORDER BY PCode";

            List<Models.InvestigationChart> investigationCharts = new List<Models.InvestigationChart>();
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = new SqlCommand(sql, connection);

            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                InvestigationChart investigationChart = new InvestigationChart();

                investigationChart.Code = reader["Pcode"].ToString();
                investigationChart.Name = reader["ShortDesc"].ToString();
                investigationChart.Price = Convert.ToDouble(reader["Charge"].ToString());
                investigationChart.SubProjectDept = reader["SubSubDeptName"].ToString();

                investigationCharts.Add(investigationChart);
            }
            return investigationCharts;
        }

        public List<InvestigationChart> GetInvestigationListByCode(string code)
        {
            string sql = "SELECT * FROM InvestigationChart WHERE PCode='" + code + "'";

            List<Models.InvestigationChart> investigationCharts = new List<Models.InvestigationChart>();
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = new SqlCommand(sql, connection);

            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                InvestigationChart investigationChart = new InvestigationChart();

                investigationChart.Code = reader["Pcode"].ToString();
                investigationChart.Name = reader["ShortDesc"].ToString();
                investigationChart.Price = Convert.ToDouble(reader["Charge"].ToString());
                investigationChart.SubProjectDept = reader["SubSubDeptName"].ToString();
                investigationChart.Project = reader["DeptName"].ToString();
                investigationChart.SubProject = reader["SubDeptName"].ToString();
                investigationChart.LessFixedAmount = Convert.ToDouble(reader["LessAmount"].ToString());
                investigationChart.RefFee = Convert.ToDouble(reader["Discount"].ToString());
                investigationChart.RefFeeType = Convert.ToInt32(reader["DiscountStatus"].ToString()); 
                investigationChart.NormalValue = reader["NormalValue"].ToString();
                investigationChart.ReportFileName = reader["RptType"].ToString();
                investigationChart.ReportDeliveryAfter = Convert.ToInt32(reader["DeliveryDuration"].ToString()); 

                investigationCharts.Add(investigationChart);
            }
            return investigationCharts;
        }
        public bool IsExitstCode(string code)
        {
            string sql = "SELECT * FROM InvestigationChart WHERE PCode='" + code + "'";

            List<InvestigationChart> InvestigationCharts = new List<InvestigationChart>();
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = new SqlCommand(sql, connection);

            SqlDataReader reader = command.ExecuteReader();

            if (reader.Read())
            {
                connection.Close();
                return true;
            }
            else
            {
                connection.Close();
                return false;
            }
        }
        public List<DepartmentInfo> GetAllProject()
        {
            string sql = "SELECT DISTINCT pno AS Description FROM project ORDER BY pno";

            List<DepartmentInfo> allDepartmentList = new List<DepartmentInfo>();
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = new SqlCommand(sql, connection);

            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                DepartmentInfo aDepartmentList = new DepartmentInfo();
                aDepartmentList.DepartmentId = reader["Description"].ToString();
                aDepartmentList.DepartmentName = reader["Description"].ToString();

                allDepartmentList.Add(aDepartmentList);
            }
            connection.Close();

            return allDepartmentList;
        }
        public List<DepartmentInfo> GetAllSubProject()
        {
            string sql = "SELECT DISTINCT SubPno AS Description FROM project ORDER BY SubPno";

            List<DepartmentInfo> allDepartmentList = new List<DepartmentInfo>();
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = new SqlCommand(sql, connection);

            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                DepartmentInfo aDepartmentList = new DepartmentInfo();
                aDepartmentList.DepartmentId = reader["Description"].ToString();
                aDepartmentList.DepartmentName = reader["Description"].ToString();

                allDepartmentList.Add(aDepartmentList);
            }
            connection.Close();

            return allDepartmentList;
        }
        public List<DepartmentInfo> GetAllSubProjectByProjectId(string projectId)
        {
            string sql = "SELECT DISTINCT SubPno AS Description FROM project WHERE pno='" + projectId + "' ORDER BY SubPno";

            List<DepartmentInfo> allDepartmentList = new List<DepartmentInfo>();
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = new SqlCommand(sql, connection);

            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                DepartmentInfo aDepartmentList = new DepartmentInfo();
                aDepartmentList.DepartmentId = reader["Description"].ToString();
                aDepartmentList.DepartmentName = reader["Description"].ToString();

                allDepartmentList.Add(aDepartmentList);
            }
            connection.Close();

            return allDepartmentList;
        }
        public List<DepartmentInfo> GetAllSubProjectDepartment()
        {
            string sql = "SELECT DISTINCT SubSubPno AS Description FROM project ORDER BY SubSubPno";

            List<DepartmentInfo> allDepartmentList = new List<DepartmentInfo>();
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = new SqlCommand(sql, connection);

            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                DepartmentInfo aDepartmentList = new DepartmentInfo();
                aDepartmentList.DepartmentId = reader["Description"].ToString();
                aDepartmentList.DepartmentName = reader["Description"].ToString();

                allDepartmentList.Add(aDepartmentList);
            }
            connection.Close();

            return allDepartmentList;
        }

        public List<DepartmentInfo> GetAllSubProjectDepartmentByProjectId(string projectId)
        {
            string sql = "SELECT DISTINCT SubSubPno AS Description FROM project WHERE pno='" + projectId + "' ORDER BY SubSubPno";

            List<DepartmentInfo> allDepartmentList = new List<DepartmentInfo>();
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = new SqlCommand(sql, connection);

            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                DepartmentInfo aDepartmentList = new DepartmentInfo();
                aDepartmentList.DepartmentId = reader["Description"].ToString();
                aDepartmentList.DepartmentName = reader["Description"].ToString();

                allDepartmentList.Add(aDepartmentList);
            }
            connection.Close();

            return allDepartmentList;
        }
    }
}