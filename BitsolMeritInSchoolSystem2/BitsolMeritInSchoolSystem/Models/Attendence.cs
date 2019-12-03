using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BitsolMeritInSchoolSystem.Models
{
    public class Attendence
    {
        public int AttendenceId { get; set; }

        //[Required, Display(Name = "Class")]
        //public int ClassId { get; set; }
        //public Class Class { get; set; }

        //[Required, Display(Name = "Section")]
        //public int SectionId { get; set; }
        //public Section Section { get; set; }

        [Required,Display(Name = "Class Section")]
        public int ClassSectionId { get; set; }
        public ClassSection ClassSection { get; set; }

        [Required, Display(Name = "Student")]
        public int StudentId { get; set; }
        public Student Student { get; set; }

        [Required, Display(Name = "Status"), RegularExpression("^[A|P|L]$"),MaxLength(1),MinLength(1)]
        public string Status { get; set; }

        [Required, Display(Name = "Date")]
        public DateTime Date { get; set; }
    }
}