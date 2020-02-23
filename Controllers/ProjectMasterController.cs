using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LowCostHousing.Data;
using LowCostHousing.Models;
using Microsoft.AspNetCore.Authorization;

namespace LowCostHousing.Controllers
{
    public class ProjectMasterController : Controller
    {
        LowCostHousingDBcontext lowCostHousingDBcontext;

        public ProjectMasterController(LowCostHousingDBcontext _lowCostHousingDBcontext)
        {
            lowCostHousingDBcontext = _lowCostHousingDBcontext;
        }
        public IActionResult Index()
        {
            var Projects = lowCostHousingDBcontext.ProjectMaster;
            return View(Projects);
        }

        #region Delete functionality
        //    Delete functionality is implemented using Ajax to avoid complete page loading
        //    [HttpGet]
        //    public IActionResult Delete(int id)
        //{
        //    var project = lowCostHousingDBcontext.projectMaster.Find(id);
        //    lowCostHousingDBcontext.Remove<ProjectMaster>(project);
        //    lowCostHousingDBcontext.SaveChanges();
        //    return RedirectToAction("Index");
        //} 
        #endregion

        [Authorize(Roles="Admin")]
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var project = lowCostHousingDBcontext.ProjectMaster.Find(id);
            return View(project);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult Edit(ProjectMaster projectMaster)
        {
            if (ModelState.IsValid)
            {
                lowCostHousingDBcontext.Update<ProjectMaster>(projectMaster);
                lowCostHousingDBcontext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult AjaxDelete(int id)
        {
            var project = lowCostHousingDBcontext.ProjectMaster.Find(id);
            lowCostHousingDBcontext.Remove<ProjectMaster>(project);
            lowCostHousingDBcontext.SaveChanges();
            var projects = lowCostHousingDBcontext.ProjectMaster;
            return PartialView(projects);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult Create(ProjectMaster projectMaster)
        {
            lowCostHousingDBcontext.Add<ProjectMaster>(projectMaster);
            lowCostHousingDBcontext.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}