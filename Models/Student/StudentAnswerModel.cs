using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Evalulet.Models.Student
{
    public class StudentAnswerModel
    {
        public List<string> FordelList { get; set; }

        public List<string> ForbedringList { get; set; }

        public bool CheckCheckBox { get; set; }

        public List<Models.Teacher.SurveyObject> SurveyForbedringer { get; set; }
        public List<Models.Teacher.SurveyObject> SurveyFordele { get; set; }
    }
}
