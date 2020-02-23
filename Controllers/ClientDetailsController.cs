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
    public class ClientDetailsController : Controller
    {
        LowCostHousingDBcontext lowCostHousingDBcontext;

        public ClientDetailsController(LowCostHousingDBcontext _lowCostHousingDBcontext)
        {
            lowCostHousingDBcontext = _lowCostHousingDBcontext;
        }
        public IActionResult Index()
        {
            int ClientRegistrationId = Convert.ToInt32(TempData["ClientRegistrationId"]);

            var details = (from c in lowCostHousingDBcontext.ClientRegistration
                           join p in lowCostHousingDBcontext.ProjectMaster on c.ProjectMasterId equals p.ProjectMasterId
                           where c.ClientRegistrationId == ClientRegistrationId
                           select new PaymentsClass
                           {
                               ClientGivenName = c.ClientGivenName,
                               ClientFamilyName = c.ClientFamilyName,
                               ProjectName = p.ProjectName,
                               MobileNumber = c.MobileNumber,
                               LotNo = c.LotNo,
                               ReferenceNumber = c.ReferenceNumber,
                               ClientRegistrationId = c.ClientRegistrationId,
                           }).ToList();

            var paymentDetails = (from py in lowCostHousingDBcontext.Payments
                                  where py.ClientRegistrationId == ClientRegistrationId
                                  select new PaymentsClass
                                  {
                                      AmountPaid = py.AmountPaid,
                                      PaidOn = py.PaidOn.ToString("dd/MM/yyyy"),
                                      Description = py.Description,
                                      FileName = py.FileName
                                  }).ToList();

            var TotalPaidAmount = (from py in lowCostHousingDBcontext.Payments
                                   where py.ClientRegistrationId == ClientRegistrationId
                                   group py by new { py.ClientRegistrationId } into g

                                   select new PaymentsClass
                                   {
                                       TotalAmount = g.Sum(s => s.AmountPaid)
                                   }).ToList();

            ViewData["Details"] = details;
            ViewData["PaymentDetails"] = paymentDetails;
            ViewData["TotalPaidAmount"] = TotalPaidAmount;

            return View();
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
    }
}