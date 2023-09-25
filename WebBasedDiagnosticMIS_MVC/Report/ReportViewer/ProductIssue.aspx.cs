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
    public partial class ProductIssue : System.Web.UI.Page
    {

        private string connectionString =
                WebConfigurationManager.ConnectionStrings["InventoryConnStr"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            string itemId = Request.QueryString["ItemId"];
            string custId = Request.QueryString["CustId"];
            string dateFrom = Request.QueryString["DateFrom"];
            string dateTo = Request.QueryString["DateTo"];

            string lcCondition = "";
            lcCondition = "AND Valid=1";

            if (custId != "")
            {
                lcCondition = lcCondition + " AND CompanyId='" + custId + "'";
            }
            if (itemId != "")
            {
                lcCondition = lcCondition + " AND ProductId='" + itemId + "'";
            }

            SqlConnection connection = new SqlConnection(connectionString);

            string query = @"SELECT * FROM VW_StockLedgerNew WHERE StockOut>0 AND TrDate BETWEEN '" + dateFrom + "' AND '" + dateTo + "' " + lcCondition + "";

            connection.Open();

            SqlDataAdapter da = new SqlDataAdapter(query, connection);
            DataTable dt = new DataTable();
            da.Fill(dt);

            ReportDocument rd = new ReportDocument();
            rd.Load(Server.MapPath("//Report//ReportFile//ProductIssue.rpt"));
            rd.SetDataSource(dt);
            CrystalReportViewer1.ReportSource = rd;
        }
    }
}