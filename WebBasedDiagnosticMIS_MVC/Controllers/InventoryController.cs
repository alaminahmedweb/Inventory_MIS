using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBasedDiagnosticMIS_MVC.Manager;
using WebBasedDiagnosticMIS_MVC.Models;

namespace WebBasedDiagnosticMIS_MVC.Controllers
{
    public class InventoryController : Controller
    {
        InventoryManager inventoryManager=new InventoryManager();
        NewPartyEntryManager newPartyEntryManager=new NewPartyEntryManager();
        public ActionResult SaveProductList()
        {

            ViewBag.GetProductCategory = inventoryManager.GetProductCategory();
            return View();
        }

        [HttpPost]
        public ActionResult SaveProductList(ProductList productList)
        {
            ViewBag.response = inventoryManager.Save(productList);
            ViewBag.GetProductCategory = inventoryManager.GetProductCategory();
            return View();
        }
        public ActionResult SaveNewParty()
        {

           // ViewBag.GetProductCategory = inventoryManager.GetProductCategory();
            return View();
        }

        [HttpPost]
        public ActionResult SaveNewParty(NewPartyEntry newPartyEntry)
        {
            ViewBag.response = newPartyEntryManager.Save(newPartyEntry);
            //ViewBag.GetProductCategory = inventoryManager.GetProductCategory();
            return View();
        }
	}
}