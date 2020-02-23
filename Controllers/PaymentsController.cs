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
using Microsoft.AspNetCore.Authorization;

namespace LowCostHousing.Controllers
{
    public class PaymentsController : Controller
    {
        LowCostHousingDBcontext lowCostHousingDBcontext;

        public PaymentsController(LowCostHousingDBcontext _lowCostHousingDBcontext)
        {
            lowCostHousingDBcontext = _lowCostHousingDBcontext;
        }
        #region IndexWithoutFilter
        //public IActionResult Index()
        //{
        //    try
        //    {
        //        var details = lowCostHousingDBcontext.ClientRegistration.Include(p => p.ProjectMaster);
        //        return View(details);
        //    }
        //    catch(Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        //[HttpGet]
        //public IActionResult Index()
        //{
        //    try
        //    { 
        //        //BindProject();
        //        var details = lowCostHousingDBcontext.ClientRegistration.Include(p => p.ProjectMaster);
        //        return View(details);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //} 
        #endregion

        public async Task<IActionResult> Index(string currentFilter, string searchString, int? pageNumber, int ProjectMasterID)
        {
            BindProject();
            ViewBag.ProjectMasterID = ProjectMasterID;
            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;

            var Clientsdata = from c in lowCostHousingDBcontext.ClientRegistration
                              join p in lowCostHousingDBcontext.ProjectMaster on c.ProjectMasterId equals p.ProjectMasterId
                              where c.ProjectMasterId == ProjectMasterID
                              select c;

            #region Commented_LINQ
            //var Clientsdata = from c in lowCostHousingDBcontext.ClientRegistration
            //                  join q in lowCostHousingDBcontext.Payments on c.ClientRegistrationId equals q.ClientRegistrationId into pyd
            //                  from x in pyd.DefaultIfEmpty()  //This line is used to make left join
            //                  join p in lowCostHousingDBcontext.ProjectMaster on c.ProjectMasterId equals p.ProjectMasterId
            //                  where c.ProjectMasterId == 1
            //                  group x by new
            //                  {
            //                      c.ClientRegistrationId,
            //                      c.ClientGivenName,
            //                      c.ClientFamilyName,
            //                      c.LotNo,
            //                      c.ReferenceNumber
            //                  } into g
            //                  select new
            //                  {
            //                      g.Key.ClientRegistrationId,
            //                      g.Key.ClientGivenName,
            //                      g.Key.ClientFamilyName,
            //                      g.Key.LotNo,
            //                      g.Key.ReferenceNumber,
            //                      Sum = g.Sum(s => s.AmountPaid)
            //                  };
            //var Clientsdata = lowCostHousingDBcontext.ClientRegistration.Include(p => p.ProjectMaster); 
            #endregion

            if (!String.IsNullOrEmpty(searchString))
            {
                Clientsdata = Clientsdata.Where(c => c.ClientGivenName.Contains(searchString)
                                       || c.ClientFamilyName.Contains(searchString));
            }
            int pageSize = 5;
            return View(await PaginatedList<ClientRegistration>.CreateAsync(Clientsdata.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        #region Commented_BindClientData
        //[HttpGet]
        //public async Task<IActionResult> BindClientData(string currentFilter, string searchString, int? pageNumber)
        //{
        //    if (searchString != null)
        //    {
        //        pageNumber = 1;
        //    }
        //    else
        //    {
        //        searchString = currentFilter;
        //    }

        //    ViewData["CurrentFilter"] = searchString;

        //    var Clientsdata = from c in lowCostHousingDBcontext.ClientRegistration
        //                      join p in lowCostHousingDBcontext.ProjectMaster on c.ProjectMasterId equals p.ProjectMasterId
        //                      select c;

        //    if (!String.IsNullOrEmpty(searchString))
        //    {
        //        Clientsdata = Clientsdata.Where(c => c.ClientGivenName.Contains(searchString)
        //                               || c.ClientFamilyName.Contains(searchString));
        //    }
        //    int pageSize = 5;
        //    return View(await PaginatedList<ClientRegistration>.CreateAsync(Clientsdata.AsNoTracking(), pageNumber ?? 1, pageSize));
        //} 
        #endregion

        [HttpGet]
        public IActionResult Pay(int Id)
        {
            var details = (from c in lowCostHousingDBcontext.ClientRegistration
                           join p in lowCostHousingDBcontext.ProjectMaster on c.ProjectMasterId equals p.ProjectMasterId
                           //join py in lowCostHousingDBcontext.Payments on c.ClientRegistrationId equals py.ClientRegistrationId into pyd
                           //from x in pyd.DefaultIfEmpty()  //This line is used to make left join
                           where c.ClientRegistrationId == Id
                           select new PaymentsClass
                           {
                               ClientGivenName = c.ClientGivenName,
                               ClientFamilyName = c.ClientFamilyName,
                               ProjectName = p.ProjectName,
                               MobileNumber = c.MobileNumber,
                               LotNo = c.LotNo,
                               ReferenceNumber = c.ReferenceNumber,
                               ClientRegistrationId = c.ClientRegistrationId,
                               //ProjectMasterId = c.ProjectMasterId
                           }).ToList();


            var paymentDetails = (from py in lowCostHousingDBcontext.Payments
                                  where py.ClientRegistrationId == Id
                                  select new PaymentsClass
                                  {
                                      AmountPaid = py.AmountPaid,
                                      PaidOn = py.PaidOn.ToString("dd/MM/yyyy"),
                                      Description = py.Description,
                                      FileName = py.FileName
                                  }).ToList();

            var TotalPaidAmount =  (from py in lowCostHousingDBcontext.Payments
                                  where py.ClientRegistrationId == Id
                                  group py by new { py.ClientRegistrationId } into g

                                  select new PaymentsClass
                                  {
                                      //g.Key.ClientRegistrationId,
                                      TotalAmount = g.Sum(s => s.AmountPaid)
                                  }).ToList();

            ViewData["Details"] = details;
            ViewData["PaymentDetails"] = paymentDetails;
            ViewData["TotalPaidAmount"] = TotalPaidAmount;
            TempData["ClientRegistrationId"] = Id;
            //TempData["ProjectMasterId"] = ProjectMasterId;
            return View();
        }

        #region Payment_Using_StoreProcedure
        //[HttpGet]
        //public IActionResult Pay(int Id)
        //{
        //    var details = lowCostHousingDBcontext.GetPaymentDetails.FromSql($"GetPaymentDetails {Id}").ToList(); 

        //    //TempData["ProjectMasterId"] = ProjectMasterId;
        //    return View(details);
        //} 
        #endregion

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult SavePayment(Double txtamountPaid, string txtPaidOn, IFormFile file,string txtDescripton)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Invoices",file.FileName);

            using (var stream = new FileStream(path, FileMode.Create))
            {
                 file.CopyToAsync(stream);
            }
            Payments Payments = new Payments();
            Payments.ClientRegistrationId = Convert.ToInt32(TempData["ClientRegistrationId"]);
            Payments.TotalAmount= Convert.ToInt32("0");
            Payments.AmountPaid = txtamountPaid;
            Payments.PaidOn = DateTime.ParseExact(txtPaidOn, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            Payments.ReceivedBy= Convert.ToInt32("1");
            Payments.CreatedOn= DateTime.ParseExact(txtPaidOn, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            Payments.CreatedBy= Convert.ToInt32("1");
            Payments.Description = txtDescripton;
            Payments.FileName = file.FileName;
            Payments.FilePath = path;
            lowCostHousingDBcontext.Add<Payments>(Payments);
            lowCostHousingDBcontext.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult DownloadInvoice(string FileName)
        {
            var Filepath = Path.Combine(
                           Directory.GetCurrentDirectory(),
                           "wwwroot/Invoices", FileName);

            byte[] fileBytes = GetFile(Filepath);
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, FileName);
        }

        byte[] GetFile(string s)
        {
            System.IO.FileStream fs = System.IO.File.OpenRead(s);
            byte[] data = new byte[fs.Length];
            int br = fs.Read(data, 0, data.Length);
            if (br != fs.Length)
                throw new System.IO.IOException(s);
            return data;
        }

        protected void BindProject()
        {
            List<ProjectMaster> projects = new List<ProjectMaster>();
            projects = (from ProjectMaster in lowCostHousingDBcontext.ProjectMaster select ProjectMaster).ToList();
            projects.Insert(0, new ProjectMaster { ProjectMasterId = 0, ProjectName = "--Select Project--" });
            ViewBag.ListOfProjects = projects;
            ViewBag.ProjectMasterID = 0;
        }

        //#region MyRegion This Query is used to show Total paid amount in Payment index form
        //from c in ClientRegistrations
        //   join q in Payments on c.ClientRegistrationId equals q.ClientRegistrationId into pyd from x in pyd.DefaultIfEmpty()  //This line is used to make left join
        //   join p in ProjectMasters on c.ProjectMasterId equals p.ProjectMasterId
        //   where c.ProjectMasterId == 1
        //   group x by new { 
        //   c.ClientRegistrationId,
        //   c.ClientGivenName,
        //   c.ClientFamilyName,
        //   c.LotNo,
        //   c.ReferenceNumber
        //    }
        //    into g
        //   select new 
        //    {
	       //    g.Key.ClientRegistrationId,
	       //    g.Key.ClientGivenName,
	       //    g.Key.ClientFamilyName,
	       //    g.Key.LotNo,
	       //    g.Key.ReferenceNumber,
	       //    Sum = g.Sum(s => s.AmountPaid)

        //    }
        //#endregion
    }
}