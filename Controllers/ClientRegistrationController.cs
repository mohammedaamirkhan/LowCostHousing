using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LowCostHousing.Data;
using LowCostHousing.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using PagedList;
using PagedList.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace LowCostHousing.Controllers
{
    public class ClientRegistrationController : Controller
    {
        LowCostHousingDBcontext lowCostHousingDBcontext;

        public ClientRegistrationController(LowCostHousingDBcontext _lowCostHousingDBcontext)
        {
            lowCostHousingDBcontext = _lowCostHousingDBcontext;
        }

        public async Task<IActionResult> Index(string currentFilter,string searchString,int? pageNumber)
        {
            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;

            var Clients = from s in lowCostHousingDBcontext.ClientRegistration
                          select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                Clients = Clients.Where(s => s.ClientGivenName.Contains(searchString)
                                       || s.ClientFamilyName.Contains(searchString));
            }
            int pageSize = 5;
            return View(await PaginatedList<ClientRegistration>.CreateAsync(Clients.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var clients = lowCostHousingDBcontext.ClientRegistration.Find(id);
            BindState();
            BindSuburb();
            BindProject();
            return View(clients);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult Edit(ClientRegistration clientRegistration)
        {
            lowCostHousingDBcontext.Update<ClientRegistration>(clientRegistration);
            lowCostHousingDBcontext.SaveChanges();
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult AjaxDelete(int id)
        {
            var client = lowCostHousingDBcontext.ClientRegistration.Find(id);
            lowCostHousingDBcontext.Remove<ClientRegistration>(client);
            lowCostHousingDBcontext.SaveChanges();
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Create()
        {
            BindState();
            BindSuburb();
            BindProject();
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult Create(ClientRegistration clientRegistration)
        {
            lowCostHousingDBcontext.Add<ClientRegistration>(clientRegistration);
            lowCostHousingDBcontext.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            var clientdetails = lowCostHousingDBcontext.ClientRegistration.Find(id);
            ViewBag.State = lowCostHousingDBcontext.State.Find(clientdetails.StateID);
            ViewBag.Suburb = lowCostHousingDBcontext.Suburb.Find(clientdetails.SuburbID);
            ViewBag.Project = lowCostHousingDBcontext.ProjectMaster.Find(clientdetails.ProjectMasterId);
            return View(clientdetails);
        }

        public void BindState()
        {
            List<State> states = new List<State>();
            states = (from State in lowCostHousingDBcontext.State select State).ToList();
            states.Insert(0, new State { StateID = 0, StateName = "--Select--" });
            ViewBag.ListOfStates = states;
        }

        public void BindSuburb()
        {
            List<Suburb> suburbs = new List<Suburb>();
            suburbs = (from Suburb in lowCostHousingDBcontext.Suburb select Suburb).ToList();
            suburbs.Insert(0, new Suburb { SuburbID = 0, SuburbName = "--Select--" });
            ViewBag.ListOfsuburbs = suburbs;
        }

        protected void BindProject()
        {
            List<ProjectMaster> projects = new List<ProjectMaster>();
            projects = (from ProjectMaster in lowCostHousingDBcontext.ProjectMaster select ProjectMaster).ToList();
            projects.Insert(0, new ProjectMaster { ProjectMasterId = 0, ProjectName = "--Select--" });
            ViewBag.ListOfProjects = projects;
        }
    }
}