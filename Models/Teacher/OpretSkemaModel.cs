using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Evalulet.Models.Teacher
{
    public class OpretSkemaModel
    {
        [Required (ErrorMessage = "Hold skal vælges")]
        public string Hold { get; set; }

        [Required(ErrorMessage = "Overskrift er påkrævet")]
        public string Overskrift { get; set; }

        public string FagNavn { get; set; }

        public string FriTekst { get; set; }

        public string DeleteSurveyCode { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        public IEnumerable<SelectListItem> GetTeam { get; set; }

        public IEnumerable<SelectListItem> GetClassName { get; set; }

        public IEnumerable<SelectListItem> GetEmail { get; set; }
        public IEnumerable<SelectListItem> GetClassEmail { get; set; }
        public IEnumerable<SelectListItem> GetDeleteSurveyCode { get; set; }
    }
}
