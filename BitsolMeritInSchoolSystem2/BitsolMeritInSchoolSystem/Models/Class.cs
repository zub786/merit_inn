using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Collections.Generic;

namespace BitsolMeritInSchoolSystem.Models
{
    public class Class
    {
        public int ClassId { get; set; }

        [Required, Display(Name = "Class")]
        public string ClassName { get; set; }

        public IList<ClassSection> ClassSection { get; set; }        
        

    }
}