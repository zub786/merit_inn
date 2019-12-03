using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BitsolMeritInSchoolSystem.Models
{
    public class User
    {
        public int Id { get; set; }
        [Required,Display(Name="User Name")]
        public string UserName { get; set; }

        [Required, Display(Name = "Password")]
        public string Password { get; set; }

        [Required, Display(Name = "Role")]
        public int UserRole { get; set; }
    }
}