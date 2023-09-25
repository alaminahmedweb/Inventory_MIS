using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBasedDiagnosticMIS_MVC.Manager;
using WebBasedDiagnosticMIS_MVC.Models;

namespace WebBasedDiagnosticMIS_MVC.Controllers
{
    public class PartyPaymentController : Controller
    {
        NewPartyEntryManager _aSupplierManager = new NewPartyEntryManager();
        PartyPaymentManager aPartyPaymentManager=new PartyPaymentManager();
        public ActionResult PartyPayment()
        {
            ViewBag.CustomerList = aPartyPaymentManager.GetSupplierList("");
            return View();
        }
        public JsonResult Save(PartyPayment aModel)
        {
            string msg = aPartyPaymentManager.Save(aModel);
            return Json(msg, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetSupplierList(string supplierId)
        {
            var data = aPartyPaymentManager.GetSupplierList(supplierId).ToList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
	}
}