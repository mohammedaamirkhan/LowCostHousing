using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LowCostHousing.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using LowCostHousing.Services;
using LowCostHousing.Models;
using LowCostHousing.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;

namespace LowCostHousing.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        UserManager<IdentityUser> userManager;
        SignInManager<IdentityUser> signInManager;
        LowCostHousingDBcontext lowCostHousingDBcontext;

        public AccountController(UserManager<IdentityUser> _userManager, SignInManager<IdentityUser> _signInManager, LowCostHousingDBcontext _lowCostHousingDBcontext)
        {
            userManager = _userManager;
            signInManager = _signInManager;
            lowCostHousingDBcontext = _lowCostHousingDBcontext;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            int ProjectMasterId = 0;
            BindProject();
            BindClientNames(ProjectMasterId);
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model,int ClientRegistrationId)
        {
            BindProject();
            ViewBag.ProjectMasterID = model.ProjectMasterId;
            BindClientNames(model.ProjectMasterId);
            ViewBag.ClientRegistrationId = ClientRegistrationId;
            if (model.UserName != null && model.Email != null && model.Password != null && model.ConfirmPassowrd != null)
            {
                IdentityUser user = new IdentityUser()
                {
                    UserName = model.UserName,
                    Email = model.Email
                };

                var result = await userManager.CreateAsync(user, model.Password);
                var resultRole = await userManager.AddToRoleAsync(user, "User");
                if (model.ProjectMasterId != 0 && ClientRegistrationId != 0)
                {
                    try
                    {
                        var updateUserId = (from p in lowCostHousingDBcontext.ClientRegistration where p.ClientRegistrationId == ClientRegistrationId select p).SingleOrDefault();
                        updateUserId.UserId = user.Id;
                        lowCostHousingDBcontext.SaveChanges();
                    }
                    catch(Exception ex)
                    {
                        throw ex;
                    }
                }
                string confirmationToken = await userManager.GenerateEmailConfirmationTokenAsync(user);
                string confirmationLink = Url.Action("ConfirmEmail", "Account", new { userid = user.Id, token = confirmationToken }, protocol: HttpContext.Request.Scheme);
                //EmailService.Send(model.Email, "Confirm your Email", "Click here to confirm your email address" + confirmationLink);
                System.IO.File.WriteAllText(@"C:\Temp\ConfirmEmail.txt", confirmationLink);
                if (result.Succeeded)
                {
                    return RedirectToAction("Login", "Account");
                }
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            var user = await userManager.FindByIdAsync(userId);

            var result = await userManager.ConfirmEmailAsync(user, token);
            if (result.Succeeded)
            {
                ViewBag.Msg = "Email confimation Succeeded!";
            }
            else
            {
                ViewBag.Msg = "Email confimation Failed!";
            }

            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            try
            {
                var result = await signInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, true);
                var UserDetails = (from x in lowCostHousingDBcontext.Users where x.UserName == model.UserName select x).SingleOrDefault();
                var registrationDetails = (from y in lowCostHousingDBcontext.ClientRegistration where y.UserId == UserDetails.Id select y).SingleOrDefault();
                if (result.Succeeded)
                {
                    if (registrationDetails == null)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        TempData["ClientRegistrationId"] = registrationDetails.ClientRegistrationId;
                        return RedirectToAction("Index", "ClientDetails");
                    }
                }
                return View();
            }
            catch(Exception ex)
            {
                throw ex;
            }
            
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        protected void BindProject()
        {
            List<ProjectMaster> projects = new List<ProjectMaster>();
            projects = (from ProjectMaster in lowCostHousingDBcontext.ProjectMaster select ProjectMaster).ToList();
            projects.Insert(0, new ProjectMaster { ProjectMasterId = 0, ProjectName = "--Select Project--" });
            ViewBag.ListOfProjects = projects;
        }

        protected void BindClientNames(int projectMasterId)
        {
            List<ClientRegistration> clientRegistration = new List<ClientRegistration>();
            clientRegistration = (from c in lowCostHousingDBcontext.ClientRegistration where c.ProjectMasterId == projectMasterId select c).ToList();
            clientRegistration.Insert(0, new ClientRegistration { ClientRegistrationId = 0, ClientGivenName = "--Select First Name--" });
            ViewBag.Clients = clientRegistration;
        }

        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}