using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Rotativa.AspNetCore;

namespace Evalulet.Controllers
{
    public class TeacherController : Controller
    {
        Storage.IStorage store = new Storage.Database();
        Services.DropdownHandler handler = new Services.DropdownHandler();
        static string SurveySelector { get; set; }

        static string cookieValue;
        static string cookieEmail;
        static string cookiePassword;
        bool cookieCheck = false;



        [HttpGet]
        public IActionResult Opret()
        {
            cookieValue = Request.Cookies["User"];
            if (cookieValue != null)
            {
                string[] words = cookieValue.Split("-");
                cookieEmail = words[0];
                cookiePassword = words[1];
                cookieCheck = store.CheckUser(cookieEmail, cookiePassword);
            }

            if (cookieCheck == true)
            {
                var getDeleteSurveyCode = store.GetSurvey();
                var team = store.GetTeam();
                var className = store.GetClassName();
                var Email = store.GetClassEmail();
                var m = new Models.Teacher.OpretSkemaModel();
                m.GetTeam = handler.GetSelectListItems(team);
                m.GetClassName = handler.GetSelectListItems(className);
                m.GetClassEmail = handler.GetSelectListItems(Email);
                m.GetDeleteSurveyCode = handler.GetSelectListItems(getDeleteSurveyCode);
                return View(m);
            }
            else
                return Redirect("/Home/Index/");
        }
        [HttpPost]
        public IActionResult Opret(Models.Teacher.OpretSkemaModel m)
        {
            if (ModelState.IsValid)
            {
                Storage.IStorage storage = new Storage.Database();

                storage.GemSkema(m.Hold, m.FagNavn, m.Overskrift, m.FriTekst, m.Email);

                return Redirect("/Teacher/SkemaGemt/");
            }
            else
            {
                return View(m);
            }
        }
        [HttpGet]
        public IActionResult SkemaGemt()
        {
            cookieValue = Request.Cookies["User"];
            if (cookieValue != null)
            {
                string[] words = cookieValue.Split("-");
                cookieEmail = words[0];
                cookiePassword = words[1];
                cookieCheck = store.CheckUser(cookieEmail, cookiePassword);
            }
            if (cookieCheck == true)
                return View();
            else
                return Redirect("/Home/Index/");
        }

        //Metoder til viewet Visskemaer
        [HttpGet]
        public IActionResult Visskemaer()
        {
            cookieValue = Request.Cookies["User"];
            if (cookieValue != null)
            {
                string[] words = cookieValue.Split("-");
                cookieEmail = words[0];
                cookiePassword = words[1];
                cookieCheck = store.CheckUser(cookieEmail, cookiePassword);
            }

            if (cookieCheck == true)
            {
                Models.Teacher.TeacherVisSkemaModel m = new Models.Teacher.TeacherVisSkemaModel();
                var survey = store.GetSurvey();

                m.GetSurvey = handler.GetSelectListItems(survey);
                return View(m);
            }
            else
                return Redirect("/Home/Index/");
        }
        [HttpPost]
        public IActionResult Visskemaer(Models.Teacher.TeacherVisSkemaModel m)
        {
            // m.ElevKode = store.GetStudentCode();
            SurveySelector = m.SelectedSurvey;
            return Redirect("/Teacher/Survey");
        }
        // Metoder til Survey
        [HttpGet]
        public IActionResult Survey()
        {
            cookieValue = Request.Cookies["User"];
            if (cookieValue != null)
            {
                string[] words = cookieValue.Split("-");
                cookieEmail = words[0];
                cookiePassword = words[1];
                cookieCheck = store.CheckUser(cookieEmail, cookiePassword);
            }
            if (cookieCheck == true)
            {
                Models.Teacher.SurveyModel m = new Models.Teacher.SurveyModel();
                try
                {
                    List<Models.Teacher.SurveyObject> surveyFordele = new List<Models.Teacher.SurveyObject>();
                    List<Models.Teacher.SurveyObject> surveyForbedringer = new List<Models.Teacher.SurveyObject>();

                    string[] survey = SurveySelector.Split("-");
                    m.ElevKode = survey[2];
                    m.Class = survey[1];
                    m.Dato = survey[3] + "-" + survey[4] + "-" + survey[5];
                    m.Team = survey[0];
                    m.FreeText = survey[6];
                    m.Header = survey[7];
                    m.Email = survey[8];
                    if (survey[2] != "")
                    {
                        store.GetSurveyAnswer(m.ElevKode, ref surveyFordele, ref surveyForbedringer);
                        m.surveyFordele = surveyFordele;
                        m.surveyForbedringer = surveyForbedringer;
                    }
                    return View(m);
                }
                catch (Exception)
                {
                    return Redirect("/Teacher/Visskemaer");
                }
            }
            else
                return Redirect("/Home/Index/");
        }
        [HttpPost]
        public IActionResult Survey(Models.Teacher.SurveyModel m)
        {
            try
            {
                List<Models.Teacher.SurveyObject> surveyFordele = new List<Models.Teacher.SurveyObject>();
                List<Models.Teacher.SurveyObject> surveyForbedringer = new List<Models.Teacher.SurveyObject>();

                string[] survey = SurveySelector.Split("-");
                m.ElevKode = survey[2];
                m.Class = survey[1];
                m.Dato = survey[3];
                m.Team = survey[0];
                if (survey[2] != "")
                {
                    store.GetSurveyAnswer(m.ElevKode, ref surveyFordele, ref surveyForbedringer);
                    m.surveyFordele = surveyFordele;
                    m.surveyForbedringer = surveyForbedringer;
                }
                return new ViewAsPdf("Survey", m);
            }
            catch (Exception)
            {
                return View(m);
            }
        }
    }
}