using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrystalDecisions.CrystalReports.Engine;

namespace WebBasedDiagnosticMIS_MVC.Report.ReportViewer
{
    public partial class BalanceReport : System.Web.UI.Page
    {
        private string connectionString =
                 WebConfigurationManager.ConnectionStrings["InventoryConnStr"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {

            SqlConnection connection = new SqlConnection(connectionString);

            string query = @"SELECT * FROM VW_StockLedger WHERE Balance>0";

            connection.Open();

            SqlDataAdapter da = new SqlDataAdapter(query, connection);
            DataTable dt = new DataTable();
            da.Fill(dt);

            ReportDocument rd = new ReportDocument();
            rd.Load(Server.MapPath("//Report//ReportFile//BalanceReport.rpt"));
            rd.SetDataSource(dt);
            CrystalReportViewer1.ReportSource = rd;
        }
    }
}