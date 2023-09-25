using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebBasedDiagnosticMIS_MVC.DBGateway;
using WebBasedDiagnosticMIS_MVC.Models;

namespace WebBasedDiagnosticMIS_MVC.Manager
{
    public class NewPartyEntryManager
    {
        private NewPartyEntryGateway newPartyEntryGateway = new NewPartyEntryGateway();

        public ActionResponse Save(NewPartyEntry newPartyEntry)
        {
            ActionResponse response = new ActionResponse();


            string message = newPartyEntryGateway.Save(newPartyEntry);

            if (message == "Saved Successfully")
            {
                response.Class = "success";
                response.Message = "Saved Successfully";
            }
            else
            {
                response.Class = "danger";
                response.Message = message;
            }
            return response;
        }


        public List<NewPartyEntry> GetSupplierList(string param)
        {
            return newPartyEntryGateway.GetSupplierList(param);
        }
    }
}