using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebBasedDiagnosticMIS_MVC.Models
{
    public class ItemReceiveEntry
    {
        public string InvoiceNo { get; set; }
        public DateTime InvoiceDate { get; set; }
        public string ReceiptNo { get; set; }
        public DateTime ReceiptDate { get; set; }
        public string TrNo { get; set; }
        public DateTime TrDate { get; set; }
        public string Remarks { get; set; }
        public double InvoiceValue { get; set; }
        public int TotalProduct { get; set; }
        public int TotalPrice { get; set; }
    }
}