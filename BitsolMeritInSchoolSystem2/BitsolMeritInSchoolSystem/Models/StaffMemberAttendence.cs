using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BitsolMeritInSchoolSystem.Models
{
    public class StaffMemberAttendence
    {
        public int StaffMemberAttendenceId { get; set; }
        public ClassSection ClassSection { get; set; }

        [Required, Display(Name = "Staff Member")]
        public int StaffMemberId { get; set; }
        public StaffMember StaffMember { get; set; }

        [Required, Display(Name = "Status"), RegularExpression("^[A|P|L]$"), MaxLength(1), MinLength(1)]
        public string Status { get; set; }

        [Required, Display(Name = "Date")]
        public DateTime Date { get; set; }


        [Display(Name = "In Time")]
        public DateTime InTime { get; set; }

        [Display(Name = "Out Time")]
        public DateTime OutTime { get; set; }

    }
}