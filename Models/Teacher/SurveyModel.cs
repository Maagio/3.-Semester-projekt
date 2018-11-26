using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Evalulet.Models.Teacher
{
    public class SurveyModel
    {
        public string ElevKode { get; set; }
        public string Team { get; set; }
        public string Class { get; set; }
        public string Dato { get; set; }
        public string FreeText { get; set; }
        public string Header { get; set; }
        public string Email { get; set; }
        public List<Models.Teacher.SurveyObject> surveyForbedringer { get; set; }
        public List<Models.Teacher.SurveyObject> surveyFordele { get; set; }

    }
}
