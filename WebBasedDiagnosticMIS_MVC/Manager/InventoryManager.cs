using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebBasedDiagnosticMIS_MVC.DBGateway;
using WebBasedDiagnosticMIS_MVC.Models;

namespace WebBasedDiagnosticMIS_MVC.Manager
{
    public class InventoryManager
    {
        InventoryGateway inventoryGateway=new InventoryGateway();
        public ActionResponse Save(ProductList productList)
        {
            ActionResponse response = new ActionResponse();


            string message = inventoryGateway.Save(productList);

            if (message == "Saved Successfully")
            {
                response.Class = "success";
                response.Message = "Saved Successfully";
            }
            else
            {
                response.Class = "danger";
                response.Message = message;
            }
            return response;

        }
 

        public List<ProductCategory> GetProductCategory()
        {
            return inventoryGateway.GetProductCategory();
        }

        public List<ProductList> GetProductList()
        {
            return inventoryGateway.GetProductList();
        }

        public List<ProductList> GetProductListById(string productId)
        {
            return inventoryGateway.GetProductListById(productId);
        }
    }
}