using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Evalulet.Models.Admin;

namespace Evalulet.Controllers
{
    public class AdminController : Controller
    {
        Storage.IStorage store = new Storage.Database();
        Services.DropdownHandler handler = new Services.DropdownHandler();
        AdminModel am = new AdminModel();
        AdminCreateTeam tm = new AdminCreateTeam();
        AdminCreateClass cm = new AdminCreateClass();

        static string cookieValue;
        static string cookieEmail;
        static string cookiePassword;
        static string securityLevel;
        bool cookieCheck = false;

    [HttpGet]
        public IActionResult Administration()
        {
            cookieValue = Request.Cookies["User"];
            if (cookieValue != null)
            {
                string[] words = cookieValue.Split("-");
                cookieEmail = words[0];
                cookiePassword = words[1];
                securityLevel = words[2];
                cookieCheck = store.CheckUser(cookieEmail, cookiePassword);
            }
            if (cookieCheck == true && securityLevel == "1")
            {
                try
                {
                    var vm = new ViewModel.AdminControllerAdministrationViewModel();
                    var email = store.GetEmail();
                    cm.GetEmail = handler.GetSelectListItems(email);
                    vm.CreateAdmin = am;
                    vm.CreateClass = cm;
                    vm.CreateTeam = tm;
                    return View(vm);
                }
                catch (Exception)
                {
                    return View();
                }
            }
            else
                return Redirect("/Home/Index/");


        }

        [HttpPost]
        public IActionResult Administration(ViewModel.AdminControllerAdministrationViewModel am)
        {
            try
            {
                if (am.CreateAdmin.CreateEmail != null)
                {

                    Services.Hashing hashing = new Services.Hashing();
                    string hash = hashing.CreateHashing(am.CreateAdmin.CreatePassword);
                    store.AddNewUser(am.CreateAdmin.CreateEmail, hash, am.CreateAdmin.CreateSecurityLevel);
                    return Redirect("/Admin/Administration");

                }
                else if (am.CreateClass.CreateClass != null)
                {

                    string classes = am.CreateClass.CreateClass;
                    store.SaveClass(classes, am.CreateClass.SelectedEmail);
                    return Redirect("/Admin/Administration");

                }
                else if (am.CreateTeam.CreateTeam != null)
                {
                    string team = am.CreateTeam.CreateTeam;
                    store.SaveTeam(team);
                    return Redirect("/Admin/Administration");
                }
                return View(am);
            }
            catch (Exception)
            {
                return View();
            }
        }
    }
}