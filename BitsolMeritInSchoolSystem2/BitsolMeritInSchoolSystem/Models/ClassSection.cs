using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BitsolMeritInSchoolSystem.Models
{
    public class ClassSection
    {
        public int ClassSectionId { get; set; }

        [Required, Display(Name = "Class")]
        public int ClassId { get; set; }
        public Class Class { get; set; }

        [Required, Display(Name = "Section")]
        public int SectionId { get; set; }
        public Section Section { get; set; }


    }
}