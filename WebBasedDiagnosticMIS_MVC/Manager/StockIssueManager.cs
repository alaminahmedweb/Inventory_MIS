using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebBasedDiagnosticMIS_MVC.DBGateway;
using WebBasedDiagnosticMIS_MVC.Models;

namespace WebBasedDiagnosticMIS_MVC.Manager
{
    public class StockIssueManager
    {
        StockIssueGateway stockIssueGateway = new StockIssueGateway();
        public string Save(List<StockIssue> aStockIssue)
        {
            return stockIssueGateway.Save(aStockIssue);
        }

        public List<ProductList> GetProductList()

        {
            return stockIssueGateway.GetProductList();
        }

        public List<ProductList> GetProductListByIdOnlyBalance(string productId)
        {
            return stockIssueGateway.GetProductListByIdOnlyBalance(productId);
        }
    }

}