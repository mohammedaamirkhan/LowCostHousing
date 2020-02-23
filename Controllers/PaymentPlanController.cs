using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LowCostHousing.Data;
using LowCostHousing.Models;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Data.SqlClient;
using System.Data;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace LowCostHousing.Controllers
{
    public class PaymentPlanController : Controller
    {
        LowCostHousingDBcontext lowCostHousingDBcontext;

        public PaymentPlanController(LowCostHousingDBcontext _lowCostHousingDBcontext)
        {
            lowCostHousingDBcontext = _lowCostHousingDBcontext;
        }
        [HttpGet]
        public IActionResult Index()
        {
            int projectMasterId = 0;
            BindReferenceNumber(projectMasterId);
            BindProject();
            return View();
        }

        [HttpPost]
        public IActionResult Index(int projectMasterId,int ReferenceNumber,int LandCost, int DevelopmentCost, DateTime StartDate, int years, int Interval,int HoldingDeposit)
        {
            double land = 0, development = 0, yearduration = 0;
            if (projectMasterId != 0 && ReferenceNumber != 0)
            {
                ViewBag.StartDate = StartDate.ToString("dd/MM/yyyy");
                ViewData["TotalLandCost"] = LandCost;
                ViewData["TotalDevelopmentCost"] = DevelopmentCost;
                ViewData["StartDate"] = StartDate.ToString("dd/MM/yyyy");
                ViewData["Totalyears"] = years;
                ViewData["Interval"] = Interval;
                ViewData["HoldingDeposit"] = HoldingDeposit;
                TempData["ClientRegistrationId"] = ReferenceNumber;
                TempData["ProjectMasterId"] = projectMasterId;
                TempData["LandCost"] = LandCost;
                TempData["DevelopmentCost"] = DevelopmentCost;
                TempData["TotalCost"] = LandCost+ DevelopmentCost;
                TempData["HoldingDeposit"] = HoldingDeposit;
            }
            BindProject();
            ViewBag.ProjectMasterID = projectMasterId;
            BindReferenceNumber(projectMasterId);
            ViewBag.ClientRegistrationId = ReferenceNumber;
            ViewBag.years = years;
            yearduration = (years / 2)*12;
            if(yearduration!=0)
            {
                land = System.Math.Round((LandCost - HoldingDeposit)/ yearduration); //System.Math.Round(LandCost / yearduration, 2);
                development = System.Math.Round(DevelopmentCost / yearduration); //System.Math.Round(DevelopmentCost / yearduration, 2);
            }
            ViewBag.LandCost = land;
            ViewBag.DevelopmentCost = development;
            return View();
        }

        protected void BindProject()
        {
            List<ProjectMaster> projects = new List<ProjectMaster>();
            projects = (from ProjectMaster in lowCostHousingDBcontext.ProjectMaster select ProjectMaster).ToList();
            projects.Insert(0, new ProjectMaster { ProjectMasterId = 0, ProjectName = "--Select Project--" });
            ViewBag.ListOfProjects = projects;
            ViewBag.ProjectMasterID = 0;
        }

        protected void BindReferenceNumber(int projectMasterId)
        {
            List<ClientRegistration> clientRegistration = new List<ClientRegistration>();
            clientRegistration = (from c in lowCostHousingDBcontext.ClientRegistration where c.ProjectMasterId == projectMasterId select c).ToList();
            clientRegistration.Insert(0, new ClientRegistration { ClientRegistrationId = 0, ReferenceNumber = 0 });
            ViewBag.ListOfReference = clientRegistration;
            ViewBag.ClientRegistrationId = 0;
        }

        [HttpPost]
        public IActionResult SaveData(PaymentPlan paymentPlan)
        {
            paymentPlan.ProjectMasterId = Convert.ToInt32(TempData["ProjectMasterId"]);
            paymentPlan.ClientRegistrationId = Convert.ToInt32(TempData["ClientRegistrationId"]);
            paymentPlan.LandCost = Convert.ToDouble(TempData["LandCost"]);
            paymentPlan.DevelopmentCost= Convert.ToDouble(TempData["DevelopmentCost"]);
            paymentPlan.TotalCost= Convert.ToDouble(TempData["TotalCost"]);
            paymentPlan.CreatedOn = DateTime.Now;
            paymentPlan.HoldingDeposit = Convert.ToDouble(TempData["HoldingDeposit"]);
            //lowCostHousingDBcontext.Add<PaymentPlan>(paymentPlan);
            //lowCostHousingDBcontext.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}