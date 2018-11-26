using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Evalulet.Models;
using System.Web;

namespace Evalulet.Controllers
{
    public class HomeController : Controller
    {
        Storage.IStorage db = new Storage.Database();
        [HttpGet]
        public IActionResult Index(Models.Home.Index m)
        {
            string cookieValue = Request.Cookies["User"];
            string surveyCodeCookie = Request.Cookies["SurveyCode"];
            if (cookieValue != null)
            {
                string[] words = cookieValue.Split("-");
                string email = words[0];
                string password = words[1];
                string errorMessage = "";
                int userType = 2;

                db.ValidateUser(email, password, ref errorMessage, ref userType);
                if (userType == 0)
                    return Redirect("/Teacher/Opret/");
                else if (userType == 1)
                    return Redirect("/Admin/Administration/");
            }
            else if (surveyCodeCookie != null)
            {
                bool check = db.CheckSurveyCode(surveyCodeCookie);
                if (check == true)
                    return Redirect("/Student/OpretSvar/");
            }
            return View();

        }
        [HttpPost]
        public IActionResult Index(Models.Home.Index m, string submit)
        {
            try
            {
                if (submit == "teacher")
                {
                    Services.Hashing hashing = new Services.Hashing();
                    string hashedPass = hashing.CreateHashing(m.Password);


                    int userType = 2;
                    string errorMessage = "";
                    db.ValidateUser(m.Email, hashedPass, ref errorMessage, ref userType);
                    m.ErrorMessage = errorMessage;
                    string cookieValue = m.Email + "-" + hashedPass + "-" + userType;
                    var cookieOptions = new Microsoft.AspNetCore.Http.CookieOptions()
                    {
                        HttpOnly = true,
                        Expires = DateTime.Now.AddDays(7)
                    };
                    if (Response != null)
                        Response.Cookies.Append("User", cookieValue, cookieOptions);
                    if (userType == 0)
                        return Redirect("/Teacher/Opret/");

                    else if (userType == 1)
                        return Redirect("/Admin/Administration/");

                    else
                        ViewBag.ErrorMessage = errorMessage;
                }
                else
                {
                    bool check = db.CheckSurveyCode(m.SurveyCode);

                    string cookieValue = m.SurveyCode;
                    var cookieOptions = new Microsoft.AspNetCore.Http.CookieOptions()
                    {
                        HttpOnly = true,
                        Expires = DateTime.Now.AddDays(7)
                    };
                    if (Response != null)
                        Response.Cookies.Append("SurveyCode", cookieValue, cookieOptions);
                    if (check == true)
                        return Redirect("/Student/OpretSvar/");
                }
            }
            catch
            {
                m.ErrorMessage = "Felterne skal udfyldes";
                ViewBag.ErrorMessage = m.ErrorMessage;
            }
            return View(m);
        }
        public IActionResult Opret()
        {
            return View();
        }
        public IActionResult Svar()
        {
            return View();
        }
        public IActionResult SvarGemt()
        {
            return View();
        }
    }
}
