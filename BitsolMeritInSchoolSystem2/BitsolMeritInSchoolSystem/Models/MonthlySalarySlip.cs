using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BitsolMeritInSchoolSystem.Models
{
    public class MonthlySalarySlip
    {
        public int MonthlySalarySlipId { get; set; }

        [Required, Display(Name = "Name")]
        public string Name { get; set; }


        [Required, Display(Name = "CNIC"), MinLength(13, ErrorMessage = "CNIC must be three digits long"), MaxLength(13, ErrorMessage = "CNIC must be three digits long")]
        public string CNIC { get; set; }
      
        [Required, Display(Name = "Salary")]
        public int Salary { get; set; }

        [Required, Display(Name = "Date")]
        public DateTime Date { get; set; }
    }
}