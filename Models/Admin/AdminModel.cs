using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Evalulet.Models.Admin
{
    public class AdminModel
    {
        public string CreateEmail { get; set; }

        public string CreatePassword { get; set; }

        public int CreateSecurityLevel { get; set; }

        public IEnumerable<SelectListItem> GetTeam { get; set; }
      
        public string SelectedTeam { get; set; }
    }
}
