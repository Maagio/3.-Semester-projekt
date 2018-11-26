using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Evalulet.Models.Teacher
{
    public class TeacherVisSkemaModel
    {
        public IEnumerable<SelectListItem> GetSurvey { get; set; }

        public string SelectedSurvey { get; set; }
    }
}
