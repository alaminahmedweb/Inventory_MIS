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
    public class DrInfoEntryGateway
    {

        private string connectionString =
            WebConfigurationManager.ConnectionStrings["DiagnosticConnStr"].ConnectionString;
        private string message;

        public string Save(DrInfoEntry drInfo)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction();
                try
                {
                    string query = "INSERT INTO DrInfo(DrCode, DrName, PresentAdd, PermanentAdd, RegistrationDate, TelephoneNo,  " +
                            " Email, TakeCommission, MRId, UserName, DeptNo,DrDOB) " +
                            " VALUES " +
                            " (@DrCode, @DrName, @PresentAdd, @PermanentAdd, @RegistrationDate, @TelephoneNo,  " +
                            " @Email, @TakeCommission, @MRId, @UserName, @DeptNo,@DrDOB)";

                    SqlCommand command = new SqlCommand(query, connection, transaction);

                    command.Parameters.Add("DrCode", SqlDbType.VarChar);
                    command.Parameters["DrCode"].Value = drInfo.DrCode;

                    command.Parameters.Add("DrName", SqlDbType.VarChar);
                    command.Parameters["DrName"].Value = drInfo.DrName;

                    command.Parameters.Add("RegistrationDate", SqlDbType.VarChar);
                    command.Parameters["RegistrationDate"].Value = drInfo.RegistrationDate;

                    command.Parameters.Add("PresentAdd", SqlDbType.VarChar);
                    command.Parameters["PresentAdd"].Value = drInfo.PresentAddress;

                    command.Parameters.Add("PermanentAdd", SqlDbType.VarChar);
                    command.Parameters["PermanentAdd"].Value = drInfo.PermanentAddress;

                    command.Parameters.Add("TelephoneNo", SqlDbType.VarChar);
                    command.Parameters["TelephoneNo"].Value = drInfo.TelephoneNo;

                    command.Parameters.Add("Email", SqlDbType.VarChar);
                    command.Parameters["Email"].Value = drInfo.Email;

                    command.Parameters.Add("TakeCommission", SqlDbType.VarChar);
                    command.Parameters["TakeCommission"].Value = drInfo.TakeCommission;

                    command.Parameters.Add("MRId", SqlDbType.VarChar);
                    command.Parameters["MRId"].Value = drInfo.MPOId;
                    
                    command.Parameters.Add("UserName", SqlDbType.VarChar);
                    command.Parameters["UserName"].Value = "";

                    command.Parameters.Add("DeptNo", SqlDbType.VarChar);
                    command.Parameters["DeptNo"].Value = drInfo.Department;

                    command.Parameters.Add("DrDOB", SqlDbType.VarChar);
                    command.Parameters["DrDOB"].Value = drInfo.DrDOB;

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

        public string Update(DrInfoEntry drInfo)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction();
                try
                {
                    string query = "UPDATE DrInfo SET DrName=@DrName, PresentAdd=@PresentAdd, PermanentAdd=@PermanentAdd, TelephoneNo=@TelephoneNo,   " +
                            " Email=@Email, TakeCommission=@TakeCommission, MRId=@MRId, DeptNo=@DeptNo,DrDOB=@DrDOB,RegistrationDate=@RegistrationDate" +
                            " WHERE DrCode=@DrCode";

                    SqlCommand command = new SqlCommand(query, connection, transaction);

                    command.Parameters.Add("DrCode", SqlDbType.VarChar);
                    command.Parameters["DrCode"].Value = drInfo.DrCode;

                    command.Parameters.Add("DrName", SqlDbType.VarChar);
                    command.Parameters["DrName"].Value = drInfo.DrName;

                    command.Parameters.Add("RegistrationDate", SqlDbType.VarChar);
                    command.Parameters["RegistrationDate"].Value = drInfo.RegistrationDate;

                    command.Parameters.Add("PresentAdd", SqlDbType.VarChar);
                    command.Parameters["PresentAdd"].Value = drInfo.PresentAddress;

                    command.Parameters.Add("PermanentAdd", SqlDbType.VarChar);
                    command.Parameters["PermanentAdd"].Value = drInfo.PermanentAddress;

                    command.Parameters.Add("TelephoneNo", SqlDbType.VarChar);
                    command.Parameters["TelephoneNo"].Value = drInfo.TelephoneNo;

                    command.Parameters.Add("Email", SqlDbType.VarChar);
                    command.Parameters["Email"].Value = drInfo.Email;

                    command.Parameters.Add("TakeCommission", SqlDbType.VarChar);
                    command.Parameters["TakeCommission"].Value = drInfo.TakeCommission;

                    command.Parameters.Add("MRId", SqlDbType.VarChar);
                    command.Parameters["MRId"].Value = drInfo.MPOId;

                    command.Parameters.Add("UserName", SqlDbType.VarChar);
                    command.Parameters["UserName"].Value = "";

                    command.Parameters.Add("DeptNo", SqlDbType.VarChar);
                    command.Parameters["DeptNo"].Value = drInfo.Department;

                    int rowAffected = command.ExecuteNonQuery();
                    if (rowAffected > 0)
                    {
                        message = "Updated Successfully";

                    }
                    else
                    {
                        message = "Failed To Update...!!!";
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
        public List<DrInfoEntry> GetAllDrList()
        {
            SqlConnection connection = new SqlConnection(connectionString);

            connection.Open();

            string query = "SELECT * FROM DrInfo  " +
                    " ORDER BY drCode";
            SqlCommand command = new SqlCommand(query, connection);
            SqlDataReader reader = command.ExecuteReader();

            List<DrInfoEntry> drInfos = new List<DrInfoEntry>();

            while (reader.Read())
            {
                DrInfoEntry aDrInfo = new DrInfoEntry();

                aDrInfo.DrCode = reader["DrCode"].ToString();
                aDrInfo.DrName = reader["DrName"].ToString();
                aDrInfo.PresentAddress = reader["PresentAdd"].ToString();
                aDrInfo.PermanentAddress = reader["PermanentAdd"].ToString();
                aDrInfo.TelephoneNo = reader["TelephoneNo"].ToString();
                aDrInfo.Email = reader["Email"].ToString();
                aDrInfo.MPOId = reader["MRId"].ToString();
                aDrInfo.Department = reader["DeptNo"].ToString();
                aDrInfo.TakeCommission = Convert.ToInt32(reader["TakeCommission"].ToString());
                drInfos.Add(aDrInfo);
            }

            connection.Close();
            return drInfos;
        }
        public List<DrInfoEntry> GetAllDrListByCode(string drCode)
        {
            SqlConnection connection = new SqlConnection(connectionString);

            connection.Open();

            string query = "SELECT a.*,b.DeptName FROM DrInfo a,DepartmentInfo b " +
                    " WHERE a.DeptNo=b.DeptNo " +
                    " AND a.DrCode='" + drCode + "' " +
                    " ORDER BY a.DrCode";
            SqlCommand command = new SqlCommand(query, connection);
            SqlDataReader reader = command.ExecuteReader();

            List<DrInfoEntry> drInfos = new List<DrInfoEntry>();

            while (reader.Read())
            {
                DrInfoEntry aDrInfo = new DrInfoEntry();

                aDrInfo.DrCode = reader["DrCode"].ToString();
                aDrInfo.DrName = reader["DrName"].ToString();
                aDrInfo.PresentAddress = reader["PresentAdd"].ToString();
                aDrInfo.PermanentAddress = reader["PermanentAdd"].ToString();
                aDrInfo.RegistrationDate = reader["RegistrationDate"].ToString();
                aDrInfo.DrDOB = reader["DrDOB"].ToString();
                aDrInfo.TelephoneNo = reader["TelephoneNo"].ToString();
                aDrInfo.Email = reader["Email"].ToString();
                aDrInfo.MPOId = reader["MRId"].ToString();
                aDrInfo.Department = reader["DeptNo"].ToString();
                aDrInfo.TakeCommission = Convert.ToInt32(reader["TakeCommission"].ToString());
                drInfos.Add(aDrInfo);
            }

            connection.Close();
            return drInfos;
        }
        public List<DepartmentInfo> GetAllDepartment()
        {
            string sql = "SELECT * FROM DepartmentInfo ORDER BY DeptNo";

            List<DepartmentInfo> allDepartmentList = new List<DepartmentInfo>();
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = new SqlCommand(sql, connection);

            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                DepartmentInfo aDepartmentList = new DepartmentInfo();
                aDepartmentList.DepartmentId = reader["DeptNo"].ToString();
                aDepartmentList.DepartmentName = reader["DeptName"].ToString();

                allDepartmentList.Add(aDepartmentList);
            }
            connection.Close();

            return allDepartmentList;
        }

        public List<MrInfoEntry> GetAllMr()
        {
            string sql = "SELECT * FROM MrInfo ORDER BY MrId";

            List<MrInfoEntry> allMrInfo = new List<MrInfoEntry>();
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = new SqlCommand(sql, connection);

            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                MrInfoEntry aMrInfo = new MrInfoEntry();
                aMrInfo.MrId = Convert.ToInt32(reader["MrId"].ToString());
                aMrInfo.MrName = reader["MRName"].ToString();
                aMrInfo.MrAddress = reader["MRAddress"].ToString();

                allMrInfo.Add(aMrInfo);
            }
            connection.Close();

            return allMrInfo;
        }
        public bool IsExistDrCode(string drCode)
        {
            SqlConnection connection = new SqlConnection(connectionString);

            connection.Open();

            string query = "SELECT * FROM DrInfo WHERE DrCode='" + drCode + "'";
            SqlCommand command = new SqlCommand(query, connection);
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
    }
}