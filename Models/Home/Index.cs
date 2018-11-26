using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Evalulet.Models.Home
{
    public class Index
    {
        //[Required]
        //[EmailAddress]
        public string Email { get; set; }
        //[Required]
        public string Password { get; set; }
        public int UserType { get; set; }
        public string ErrorMessage { get; set; }
        public string SurveyCode { get; set; }
    }
}
