using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BitsolMeritInSchoolSystem.Models
{
    public class DailyExpenditure
    {
        public int DailyExpenditureId { get; set; }
        [Required, Display(Name = "Expense Description")]
        public string ExpenditureDescription { get; set; }

        [Required, Display(Name = "Expense Amount")]
        public int ExpenditureAmount { get; set; }

        [Required, Display(Name = "Date")]
        public DateTime Date { get; set; }
    }
}