using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using LowCostHousing.Services;
using LowCostHousing.Models;
using LowCostHousing.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using LowCostHousing.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace LowCostHousing.Controllers
{
    [Authorize(Roles ="Admin")]
    public class AccountManagerController : Controller
    {
        UserManager<IdentityUser> userManager;
        RoleManager<IdentityRole> roleManager;
        LowCostHousingDBcontext lowCostHousingDBcontext;

        public AccountManagerController(UserManager<IdentityUser> _userManager, RoleManager<IdentityRole> _roleManager, LowCostHousingDBcontext _lowCostHousingDBcontext)
        {
            userManager = _userManager;
            roleManager = _roleManager;
            lowCostHousingDBcontext = _lowCostHousingDBcontext;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult AssignRole()
        {
            var Users = userManager.Users.ToList();
            var roles = roleManager.Roles.ToList();

            List<string> listRoles = new List<string>();
            foreach (var item in roles)
            {
                listRoles.Add(item.Name);
            }

            List<AssignRoleViewModel> List = new List<AssignRoleViewModel>();
            foreach (var item in Users)
            {
                AssignRoleViewModel model = new AssignRoleViewModel();
                model.UserId = item.Id;
                model.UserName = item.UserName;
                model.Email = item.Email;
                model.RoleName = userManager.GetRolesAsync(item).Result.Count != 0 ? userManager.GetRolesAsync(item).Result[0] : "";
                model.Roles = listRoles;
                List.Add(model);
            }
            return View(List);
        }

        [HttpPost]
        public async Task<IActionResult> AssignRole(string UserId, string RoleName)
        {
            var user = userManager.FindByIdAsync(UserId).Result;
            var roles = roleManager.Roles.ToList();

            List<string> listRoles = new List<string>();
            foreach (var item in roles)
            {
                listRoles.Add(item.Name);
            }

            var removeRole = await userManager.RemoveFromRolesAsync(user, listRoles);
            var result = await userManager.AddToRoleAsync(user, RoleName);
            if(result.Succeeded)
            {
                return RedirectToAction("AssignRole");
            }
            return View();
        }
    }
}