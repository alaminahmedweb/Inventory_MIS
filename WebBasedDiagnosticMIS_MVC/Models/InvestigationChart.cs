using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebBasedDiagnosticMIS_MVC.Models
{
    public class InvestigationChart
    {
        public string Code { get; set; }

        public string Name { get; set; }

        public double Price { get; set; }

        public string Project { get; set; }

        public string SubProject { get; set; }

        public string SubProjectDept { get; set; }

        public double LessFixedAmount { get; set; }

        public double RefFee { get; set; }

        public int RefFeeType { get; set; }

        public string NormalValue { get; set; }

        public string ReportFileName { get; set; }

        public int ReportDeliveryAfter { get; set; }

        public string VaqStatus { get; set; }

        public string UserName { get; set; }
    }
}