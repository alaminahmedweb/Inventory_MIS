using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebBasedDiagnosticMIS_MVC.DBGateway;
using WebBasedDiagnosticMIS_MVC.Models;

namespace WebBasedDiagnosticMIS_MVC.Manager
{
    public class InvestigationChartManager
    {
        InvestigationChartGateway investigationChartGateway = new InvestigationChartGateway();
        public ActionResponse Save(InvestigationChart investigationChart)
        {
            ActionResponse response = new ActionResponse();
            if (investigationChartGateway.IsExitstCode(investigationChart.Code))
            {
                response.Class = "danger";
                response.Message = "Code Already Exists...";
                return response;
            }

            string message = investigationChartGateway.Save(investigationChart);

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
        public bool IsExitstCode(string code)
        {
            return investigationChartGateway.IsExitstCode(code);
        }

        public List<InvestigationChart> GetAllInvestigationList()
        {
            return investigationChartGateway.GetAllInvestigationList();
        }

        public List<InvestigationChart> GetInvestigationListByCode(string code)
        {
            return investigationChartGateway.GetInvestigationListByCode(code);
        }

        public List<DepartmentInfo> GetAllProject()
        {
            return investigationChartGateway.GetAllProject();
        }

        public List<DepartmentInfo> GetAllSubProject()
        {
            return investigationChartGateway.GetAllSubProject();
        }


        public List<DepartmentInfo> GetAllSubProjectByProjectId(string projectId)
        {
            return investigationChartGateway.GetAllSubProjectByProjectId(projectId);
        }
        public List<DepartmentInfo> GetAllSubProjectDepartment()
        {
            return investigationChartGateway.GetAllSubProjectDepartment();
        }

        public List<DepartmentInfo> GetAllSubProjectDepartmentByProjectId(string projectId)
        {
            return investigationChartGateway.GetAllSubProjectDepartmentByProjectId(projectId);
        }

    }
}