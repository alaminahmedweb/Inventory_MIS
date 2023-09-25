using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBasedDiagnosticMIS_MVC.Manager;
using WebBasedDiagnosticMIS_MVC.Models;

namespace WebBasedDiagnosticMIS_MVC.Controllers
{
    public class UserLoginController : Controller
    {
        UserLoginManager userLoginManager=new UserLoginManager();
        //
        // GET: /UserLogin/
        [HttpGet]
        public ActionResult UserLogin()
        {
            return View();
        }
        [HttpPost]
        public ActionResult UserLogin(UserLogin userLogin)
        {
            string message = userLoginManager.IsValidUser(userLogin);
            if (message == "Successfully Loged in")
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return RedirectToAction("UserLogin", "UserLogin");
            }
            //return View();
        }
	}
}