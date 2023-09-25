using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBasedDiagnosticMIS_MVC.Manager;
using WebBasedDiagnosticMIS_MVC.Models;

namespace WebBasedDiagnosticMIS_MVC.Controllers
{
    public class StockReceiveController : Controller
    {
        StockReceiveManager stockReceiveManager = new StockReceiveManager();
        NewPartyEntryManager _aSupplierManager=new NewPartyEntryManager();
        InventoryManager inventoryManager = new InventoryManager();

        [HttpGet]
        public ActionResult SaveStockReceive()
        {
            ViewBag.GetProductList = inventoryManager.GetProductList();
            ViewBag.SupplierList = _aSupplierManager.GetSupplierList("");
            return View();
        }

        public JsonResult Save(List<StockReceive> aModel)
        {
            string msg = stockReceiveManager.Save(aModel);
           // stockReceiveManager.GetBarcodePrint(aModel.ElementAt(0).InvoiceNo, aModel.ElementAt(0).InvoiceDate);
          //  _aDbConnection.PrintReport("BarcodeList.rpt", "DT_TEMPORARY_BARCODE_PRINT", "", "tbl_TEMPORARY_BARCODE_PRINT", "BarcodeList", "", "V");
            return Json(msg, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetSupplierList(string supplierId)
        {
            var data = _aSupplierManager.GetSupplierList(supplierId).ToList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetProductListById(string productId)
        {
            var data = inventoryManager.GetProductListById(productId).ToList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }



        //public JsonResult GetDetailsSupplierListById(int param)
        //{
        //    var suppMaster = _aSupplierManager.GetSupplierList(param);
        // //   var suppDetails = _aSupplierManager.GetDetailsSupplierListById(param);
        // //   var data = new { suppMaster = suppMaster, suppDetails = suppDetails };
        // //   return Json(data, JsonRequestBehavior.AllowGet);
        //}
	}
}