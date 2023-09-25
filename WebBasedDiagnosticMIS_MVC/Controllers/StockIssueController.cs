using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBasedDiagnosticMIS_MVC.Manager;
using WebBasedDiagnosticMIS_MVC.Models;

namespace WebBasedDiagnosticMIS_MVC.Controllers
{
    public class StockIssueController : Controller
    {
        InventoryManager inventoryManager = new InventoryManager();
        StockIssueManager stockIssueManager=new StockIssueManager();

        // GET: /StockIssue/
        public ActionResult StockIssue()
        {
            ViewBag.GetProductList = stockIssueManager.GetProductList();
            return View();
        }


        public JsonResult Save(List<StockIssue> aModel)
        {
            string msg = stockIssueManager.Save(aModel);
            // stockReceiveManager.GetBarcodePrint(aModel.ElementAt(0).InvoiceNo, aModel.ElementAt(0).InvoiceDate);
            //  _aDbConnection.PrintReport("BarcodeList.rpt", "DT_TEMPORARY_BARCODE_PRINT", "", "tbl_TEMPORARY_BARCODE_PRINT", "BarcodeList", "", "V");
            return Json(msg, JsonRequestBehavior.AllowGet);
        }
       
        public JsonResult GetProductListById(string productId)
        {
            var data = inventoryManager.GetProductListById(productId).ToList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetProductListByIdOnlyAvailable(string productId)
        {
            var data = stockIssueManager.GetProductListByIdOnlyBalance(productId).ToList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
	}
}