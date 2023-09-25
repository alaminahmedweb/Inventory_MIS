using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebBasedDiagnosticMIS_MVC.DBGateway;
using WebBasedDiagnosticMIS_MVC.Models;

namespace WebBasedDiagnosticMIS_MVC.Manager
{
    public class StockReceiveManager
    {
        StockReceiveGateway stockReceiveGateway=new StockReceiveGateway();
        public string Save(List<StockReceive> aStockReceiveNewItemModel)
        {
            return stockReceiveGateway.Save(aStockReceiveNewItemModel);
        }
    }
}