using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebBasedDiagnosticMIS_MVC.DBGateway;
using WebBasedDiagnosticMIS_MVC.Models;

namespace WebBasedDiagnosticMIS_MVC.Manager
{
    public class PartyPaymentManager
    {
        private PartyPaymentGateway partyPaymentGateway = new PartyPaymentGateway();

        public string Save(PartyPayment aPartyPayment)
        {
            return partyPaymentGateway.Save(aPartyPayment);
        }

        public List<PartyPayment> GetSupplierList(string param)
        {
            return partyPaymentGateway.GetSupplierList(param);
        }
    }
}