using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebBasedDiagnosticMIS_MVC.Models
{
    public class PartyPayment:NewPartyEntry
    {
        public double TotalDueAmt { get; set; }
        public double PaymentAmt { get; set; }
        public double LessAmt { get; set; }
        public double NetPayableAmt { get; set; }
        public string ReceiptNo { get; set; }
        public DateTime ReceiptDate { get; set; }
        public string Remarks { get; set; }

    }
}