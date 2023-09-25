using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBasedDiagnosticMIS_MVC.Manager;
using WebBasedDiagnosticMIS_MVC.Models;

namespace WebBasedDiagnosticMIS_MVC.Controllers
{
    public class NewUserLoginController : Controller
    {

        UserLoginManager userLoginManager = new UserLoginManager();
     
        [HttpGet]
        public ActionResult UserLoginNew()
        {
            return View();
        }

        [HttpPost]
        public ActionResult UserLoginNew(UserLogin userLogin)
        {
            string message = userLoginManager.IsValidUser(userLogin);
            if (message == "Successfully Loged in")
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return RedirectToAction("UserLoginNew", "NewUserLogin");
            }
            //return View();
        }

    }
}