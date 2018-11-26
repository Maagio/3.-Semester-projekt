using Evalulet.Models.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Evalulet.ViewModel
{
    public class AdminControllerAdministrationViewModel
    {
        public AdminModel CreateAdmin{ get; set; }
        public AdminCreateClass CreateClass { get; set; }
        public AdminCreateTeam CreateTeam{ get; set; }
    }
}
