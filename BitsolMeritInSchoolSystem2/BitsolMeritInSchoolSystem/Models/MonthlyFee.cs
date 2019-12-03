using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BitsolMeritInSchoolSystem.Models
{
    public class MonthlyFee
    {
        public int MonthlyFeeId { get; set; }

        //[Required, Display(Name = "Class")]
        //public int ClassId { get; set; }
        //public Class Class { get; set; }

        //[Required, Display(Name = "Section")]
        //public int SectionId { get; set; }
        //public Section Section { get; set; }

        [Required, Display(Name = "Class Section")]
        public int ClassSectionId { get; set; }
        public ClassSection ClassSection { get; set; }


        [Required, Display(Name = "Student Name")]
        public int StudentId { get; set; }
        public Student Student { get; set; }

        [Required, Display(Name = "Fee")]
        public int FeeAmount { get; set; }

        [Required, Display(Name = "Submit Date")]
        public DateTime Date { get; set; }




    }
}