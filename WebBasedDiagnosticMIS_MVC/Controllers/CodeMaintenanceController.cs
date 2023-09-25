using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using  System.Web.Services;
using System.Web.Script.Serialization;
using Microsoft.Ajax.Utilities;
using WebBasedDiagnosticMIS_MVC.Manager;
using WebBasedDiagnosticMIS_MVC.Models;

namespace WebBasedDiagnosticMIS_MVC.Controllers
{
    public class CodeMaintenanceController : Controller
    {
        InvestigationChartManager investigationChartManager=new InvestigationChartManager();
        DrInfoEntryManager drInfoEntryManager=new DrInfoEntryManager();
        public ActionResult SaveInvestigationChart()
        {
            ViewBag.GetAllProject = investigationChartManager.GetAllProject();
            ViewBag.GetAllSubProject = investigationChartManager.GetAllSubProject();
            ViewBag.GetAllSubProjectDept = investigationChartManager.GetAllSubProjectDepartment();
            ViewBag.InvestigationList = investigationChartManager.GetAllInvestigationList();
            return View();
        }

        [HttpPost]
        public ActionResult SaveInvestigationChart(InvestigationChart investigationChart)
        {
            ViewBag.response = investigationChartManager.Save(investigationChart);
            ViewBag.GetAllProject = investigationChartManager.GetAllProject();
            ViewBag.GetAllSubProject = investigationChartManager.GetAllSubProject();
            ViewBag.GetAllSubProjectDept = investigationChartManager.GetAllSubProjectDepartment();
            ViewBag.InvestigationList = investigationChartManager.GetAllInvestigationList();
            return View();
        }

        [HttpPost]
        public ActionResult SaveDrInfo(DrInfoEntry drInfoEntry)
        {
            ViewBag.response = drInfoEntryManager.Save(drInfoEntry);
            ViewBag.GetAllDepartment = drInfoEntryManager.GetAllDepartment();
            ViewBag.GetAllMr = drInfoEntryManager.GetAllMr();
            ViewBag.GetAllDrList = drInfoEntryManager.GetAllDrList();

            return View();
        }

     
        [HttpGet]
        public ActionResult SaveDrInfo()
        {
            ViewBag.GetAllDepartment = drInfoEntryManager.GetAllDepartment();
            ViewBag.GetAllMr = drInfoEntryManager.GetAllMr();
            ViewBag.GetAllDrList = drInfoEntryManager.GetAllDrList();
            return View();
        }

        public JsonResult GetAllInvestigationList()
        {
            var investigationChartList = investigationChartManager.GetAllInvestigationList().ToList();
            return Json(investigationChartList, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetInvestigationListByCode(string code)
        {
            var investigationChartList = investigationChartManager.GetInvestigationListByCode(code).ToList();
            return Json(investigationChartList, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetSubDepartmentByProjectId(string projectId)
        {
            var investigationChartList = investigationChartManager.GetAllSubProjectByProjectId(projectId).ToList();
            return Json(investigationChartList, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetAllSubProjectDepartmentByProjectId(string projectId)
        {
            var investigationChartList = investigationChartManager.GetAllSubProjectDepartmentByProjectId(projectId).ToList();
            return Json(investigationChartList, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetAllDrListByCode(string code)
        {
            var drList = drInfoEntryManager.GetAllDrListByCode(code).ToList();
            return Json(drList, JsonRequestBehavior.AllowGet);
        }
    }
}