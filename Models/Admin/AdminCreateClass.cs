using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Evalulet.Models.Admin
{
    public class AdminCreateClass
    {
        public string CreateClass { get; set; }

        public IEnumerable<SelectListItem> GetEmail { get; set; }
        
        public string SelectedEmail { get; set; }
    }
}
