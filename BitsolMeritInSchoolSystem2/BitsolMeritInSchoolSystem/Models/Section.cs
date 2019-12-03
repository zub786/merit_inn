using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BitsolMeritInSchoolSystem.Models
{
    public class Section
    {
        public int SectionId { get; set; }

        [Required, Display(Name = "Section")]
        public string SectionName { get; set; }

        public IList<ClassSection> ClassSection { get; set; }

        //[Required, Display(Name = "Class")]
        //public int ClassId { get; set; }
        //public Class Class { get; set; }

        //public IList<Class> Class { get; set; }
    }
}