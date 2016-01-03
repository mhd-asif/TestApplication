using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TestApplication.Models
{
    public class UserDetails
    {
        [StringLength(7, MinimumLength = 2, ErrorMessage = "The username should be between 2 to 7 characters")]
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}