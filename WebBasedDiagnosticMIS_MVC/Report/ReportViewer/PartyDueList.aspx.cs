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
    public partial class PartyDueList : System.Web.UI.Page
    {

        private string connectionString =
                WebConfigurationManager.ConnectionStrings["InventoryConnStr"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            string custId = Request.QueryString["CustId"];
            
            string lcCondition = "";
            
            if (custId != "")
            {
                lcCondition = lcCondition + " AND PartyId='" + custId + "'";
            }
            
            SqlConnection connection = new SqlConnection(connectionString);

            string query = @"SELECT * FROM VW_PartyWiseDue WHERE Due>0 " + lcCondition + "";

            connection.Open();

            SqlDataAdapter da = new SqlDataAdapter(query, connection);
            DataTable dt = new DataTable();
            da.Fill(dt);

            ReportDocument rd = new ReportDocument();
            rd.Load(Server.MapPath("//Report//ReportFile//PartyDueList.rpt"));
            rd.SetDataSource(dt);
            CrystalReportViewer1.ReportSource = rd;
        }
    }
}