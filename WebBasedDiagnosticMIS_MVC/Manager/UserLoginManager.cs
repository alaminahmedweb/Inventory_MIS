using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebBasedDiagnosticMIS_MVC.DBGateway;
using WebBasedDiagnosticMIS_MVC.Models;

namespace WebBasedDiagnosticMIS_MVC.Manager
{
    public class UserLoginManager
    {
        UserLoginGateway userLoginGateway=new UserLoginGateway();
        public string IsValidUser(UserLogin userLogin)
        {
            return userLoginGateway.IsValidUser(userLogin);
        }
    }
}