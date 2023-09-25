using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebBasedDiagnosticMIS_MVC.Models
{
    public class NewPartyEntry :ProductList
    {
        public string PartyId { get; set; }
        public string PartyName { get; set; }
        public string PartyAddress { get; set; }
        public string ContactNo { get; set; }

    }
}