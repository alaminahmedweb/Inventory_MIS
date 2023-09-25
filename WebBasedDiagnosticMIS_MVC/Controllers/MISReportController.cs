using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBasedDiagnosticMIS_MVC.DBGateway;
using WebBasedDiagnosticMIS_MVC.Manager;

namespace WebBasedDiagnosticMIS_MVC.Controllers
{
    public class MISReportController : Controller
    {
        NewPartyEntryManager _aSupplierManager = new NewPartyEntryManager();
        InventoryManager inventoryManager = new InventoryManager();
        readonly DBConnectionGateway _aDb = new DBConnectionGateway();

        public ActionResult MISReport()
        {
            ViewBag.ProductList = inventoryManager.GetProductList();
            ViewBag.SupplierList = _aSupplierManager.GetSupplierList("");
            return View();
        }


        public string PrintReport(ReportPrint aPrint)
        {
            string lcCondition = "'" + aPrint.DateFrom + "','" + aPrint.DateTo + "'";
            switch (aPrint.ReportFileName)
            {
                case "BalanceReport":
                    lcCondition = lcCondition + "," + aPrint.CustId;
                    //Response.Redirect("~/Report/ReportViewer/BalanceReport.aspx");
                    //Db.PrintReport("NewItemInvoiceWiseSales.rpt", "DT_GET_INVOICEWISE_REPORT", lcCondition, "SP_GET_INVOICEWISE_REPORT", "Invoice Wise Sales", "Reporting date from " + aPrint.DateFrom + " to " + aPrint.DateTo, "S");
                    break;
                case "ProductWiseSales":
                    lcCondition = "WHERE InvoiceDate BETWEEN '" + aPrint.DateFrom + "' AND '" + aPrint.DateTo + "'";
                    if (aPrint.ItemId != "0") { lcCondition += "AND  ItemId='" + aPrint.ItemId + "'"; }
                    if (aPrint.SuppId != "0") { lcCondition += "AND  SuppId='" + aPrint.SuppId + "'"; }

                    //_aDb.PrintReport("NewItemProductWiseSales.rpt", "DT_ITEMWISE_SALES", lcCondition, "VW_ITEMWISE_SALES", "Product Wise Sales", "Reporting date from " + aPrint.DateFrom + " to " + aPrint.DateTo, "V");
                    break;
                case "ProfitSharing":
                    lcCondition = "WHERE InvoiceDate BETWEEN '" + aPrint.DateFrom + "' AND '" + aPrint.DateTo + "'";
                    //_aDb.PrintReport("NewItemProfitSharing.rpt", "DT_ITEMWISE_SALES", lcCondition, "VW_ITEMWISE_SALES", "Profit Sharing", "Reporting date from " + aPrint.DateFrom + " to " + aPrint.DateTo, "V");
                    break;
                case "StockReportSummary":
                    lcCondition = "";
                    if ((aPrint.ItemId != "0") && (aPrint.CustId != "0")) { lcCondition = "WHERE ItemId='" + aPrint.ItemId + "' AND  SuppId='" + aPrint.CustId + "'"; }
                    if ((aPrint.ItemId == "0") && (aPrint.CustId != "0")) { lcCondition = "WHERE SuppId='" + aPrint.CustId + "'"; }
                    if ((aPrint.ItemId != "0") && (aPrint.CustId == "0")) { lcCondition = "WHERE ItemId='" + aPrint.ItemId + "'"; }
                    //_aDb.PrintReport("NewItemStockSummary.rpt", "DT_GET_BAL_QTY_BY_ITEM_ID", lcCondition, "VW_GET_BAL_QTY_BY_ITEM_ID", "Stock Report Summary", "", "V");
                    break;

            }
            return "";
        }
        public class ReportPrint
        {
            public string DateFrom { get; set; }
            public string DateTo { get; set; }
            public string CustId { get; set; }
            public string ItemId { get; set; }
            public string SuppId { get; set; }
            public string ReportFileName { get; set; }


        }
	}
}