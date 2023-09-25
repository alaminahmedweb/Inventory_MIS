using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebBasedDiagnosticMIS_MVC.Models
{
    public class ProductList
    {
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductCategory { get; set; }
        public double UnitPrice { get; set; }
        public string Unit { get; set; }
        public int ReminderStock { get; set; }
        public string Type { get; set; }
        public string AssetType { get; set; }
        public double Balance { get; set; }
    }
}