using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebBasedDiagnosticMIS_MVC.Models
{
    public class DrInfoEntry
    {
        public string DrCode { get; set; }
        public string DrName { get; set; }
        public string PresentAddress { get; set; }
        public string PermanentAddress { get; set; }
        public string Department { get; set; }
        public string RegistrationDate { get; set; }
        public string DrDOB { get; set; }
        public string TelephoneNo { get; set; }
        public string Email { get; set; }
        public int TakeCommission { get; set; }
        public string MPOId { get; set; }

    }
}