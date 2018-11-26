using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Evalulet.Controllers
{
    public class StudentController : Controller
    {
        static string surveyCode = "";
        Storage.IStorage storage = new Storage.Database();
        bool cookieCheck = false;
        [HttpGet]
        public IActionResult OpretSvar()
        {
            surveyCode = Request.Cookies["SurveyCode"];
            cookieCheck = storage.CheckSurveyCode(surveyCode);
            if (cookieCheck == true)
                return View();
            else
                return Redirect("/Home/Index/");
        }
        [HttpGet]
        public IActionResult SvarGemt()
        {
            surveyCode = Request.Cookies["SurveyCode"];
            cookieCheck = storage.CheckSurveyCode(surveyCode);
            if (cookieCheck == true)
                return View();
            else
                return Redirect("/Home/Index/");
        }

        [HttpPost]
        public IActionResult OpretSvar(Models.Student.StudentAnswerModel m)
        {
            try
            {

                storage.GemStudentAnswer(m.FordelList, m.ForbedringList, surveyCode);

                return Redirect("/Student/OpretSvarGemt/");
            }
            catch
            {
                return View(m);
            }
        }

        static List<Models.Teacher.SurveyObject> surveyFordele = new List<Models.Teacher.SurveyObject>();
        static List<Models.Teacher.SurveyObject> surveyForbedringer = new List<Models.Teacher.SurveyObject>();
        Storage.IStorage store = new Storage.Database();
        [HttpGet]
        public IActionResult Svar()
        {
            Models.Student.StudentAnswerModel m = new Models.Student.StudentAnswerModel();
            surveyCode = Request.Cookies["SurveyCode"];
            cookieCheck = storage.CheckSurveyCode(surveyCode);
            if (cookieCheck == true)
            {
                try
                {
                    store.GetSurveyAnswer(surveyCode, ref surveyFordele, ref surveyForbedringer);
                    m.SurveyFordele = surveyFordele;
                    m.SurveyForbedringer = surveyForbedringer;

                    return View(m);
                }
                catch (Exception)
                {
                    return Redirect("/Student/Svar/");
                }
            }
            else
                return Redirect("/Home/Index/");
        }
        [HttpPost]
        public IActionResult Svar(Models.Student.StudentAnswerModel m)
        {
            try
            {
                foreach (var item in surveyFordele)
                {
                    if (m.CheckCheckBox == true)
                    {
                        store.GemStudentCheckBox(item.Answer);
                    }
                }
                foreach (var item in surveyForbedringer)
                {
                    if (m.CheckCheckBox == true)
                    {
                        store.GemStudentCheckBox(item.Answer);
                    }
                }


                Response.Cookies.Delete("SurveyCode");

                return Redirect("/Student/SvarGemt/");
            }
            catch (Exception)
            {

                return Redirect("/Student/Svar/");
            }
        }
        [HttpGet]
        public IActionResult OpretSvarGemt()
        {
            surveyCode = Request.Cookies["SurveyCode"];
            cookieCheck = storage.CheckSurveyCode(surveyCode);
            if (cookieCheck == true)
                return View();
            else
                return Redirect("/Home/Index/");
        }
    }
}



