using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Services;
using System.Web.Script.Serialization;
using WebBasedDiagnosticMIS_MVC.Models;


namespace WebBasedDiagnosticMIS_MVC.WebService
{
    /// <summary>
    /// Summary description for InvestigationChart
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class InvestigationChart : System.Web.Services.WebService
    {
         [WebMethod]
        public void GetAllInvestigationList()
        {
             string connectionString =
                WebConfigurationManager.ConnectionStrings["DiagnosticConnStr"].ConnectionString;
        
            string sql = "SELECT * FROM InvestigationChart ORDER BY PCode";

            List<Models.InvestigationChart> investigationCharts = new List<Models.InvestigationChart>();
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = new SqlCommand(sql, connection);

            SqlDataReader reader = command.ExecuteReader();

            while(reader.Read())
            {
                Models.InvestigationChart investigationChart = new Models.InvestigationChart();

                investigationChart.Code = reader["Pcode"].ToString();
                investigationChart.Name = reader["ShortDesc"].ToString();
                investigationChart.Price = Convert.ToDouble(reader["Charge"].ToString());
                investigationChart.SubProjectDept = reader["SubSubDeptName"].ToString();

                investigationCharts.Add(investigationChart);
            }

             JavaScriptSerializer js=new JavaScriptSerializer();
             Context.Response.Write(js.Serialize(investigationCharts));
        }
    }
}
