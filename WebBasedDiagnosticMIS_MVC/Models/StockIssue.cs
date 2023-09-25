using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebBasedDiagnosticMIS_MVC.Models
{
    public class StockIssue : NewPartyEntry 
    {
        public string SlipNo { get; set; }
        public DateTime TrDate { get; set; }
        public string ReceiptNo { get; set; }
        public DateTime ReceiptDate { get; set; }
        public string Remarks { get; set; }
        public double InvoiceValue { get; set; }
        public int TotalProduct { get; set; }
        public int TotalPrice { get; set; }
        public int Quantity { get; set; }
        public int StockStatus { get; set; }
        public string UserName { get; set; }
        public string EntryTime { get; set; }
        public int Valid { get; set; }
    }
}