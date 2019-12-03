using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BitsolMeritInSchoolSystem.Models
{
    public class AnnualCharges
    {
        public int AnnualChargesId { get; set; }
        [Required, Display(Name = "Charges Description")]
        public string AnnualChargesDescription { get; set; }

        [Required, Display(Name = "Paid Amount")]
        public int PaidAmount { get; set; }

        [Display(Name = "Remaining Charges Amount")]
        public int RemainingChargesAmount { get; set; }

        [Required, Display(Name = " Submission Date")]
        public DateTime Date { get; set; }

        [Required, Display(Name = "Student")]
        public int StudentId { get; set; }      
        public Student Student { get; set; }


    }
}