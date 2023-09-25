using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebBasedDiagnosticMIS_MVC.DBGateway;
using WebBasedDiagnosticMIS_MVC.Models;

namespace WebBasedDiagnosticMIS_MVC.Manager
{
    public class DrInfoEntryManager
    {
        DrInfoEntryGateway drInfoGateway = new DrInfoEntryGateway();

        public ActionResponse Save(DrInfoEntry drInfo)
        {
            ActionResponse response = new ActionResponse();
            if (drInfoGateway.IsExistDrCode(drInfo.DrCode))
            {
                response.Class = "danger";
                response.Message = "Code Already Exists...";
                return response;
            }

            string message = drInfoGateway.Save(drInfo);

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

        public string Update(DrInfoEntry drInfo)
        {
            return drInfoGateway.Update(drInfo);
        }

        public List<DrInfoEntry> GetAllDrList()
        {
            return drInfoGateway.GetAllDrList();
        }

        public List<MrInfoEntry> GetAllMr()
        {
            return drInfoGateway.GetAllMr();
        }

        public List<DrInfoEntry> GetAllDrListByCode(string drCode)
        {
            return drInfoGateway.GetAllDrListByCode(drCode);
        }

        public List<DepartmentInfo> GetAllDepartment()
        {
            return drInfoGateway.GetAllDepartment();
        }

        public bool IsExistDrCode(string drCode)
        {
            return drInfoGateway.IsExistDrCode(drCode);
        }
    }
}