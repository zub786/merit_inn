using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BitsolMeritInSchoolSystem.Models
{
    public class StaffMember
    {
        public int StaffMemberId { get; set; }
        [Required, Display(Name = "Staff Member Name")]
        public string StaffMemberName { get; set; }

        [Required, Display(Name = "Father Name")]
        public string StaffMemberFatherName { get; set; }

        [Required, Display(Name = "Contact No"), DataType(DataType.PhoneNumber)]
        public string StaffMemberContactNo { get; set; }

        [Required, Display(Name = "Bay-Form No"), MinLength(13, ErrorMessage = "CNIC must have 13 digits exactly"), MaxLength(13, ErrorMessage = "CNIC must have 13 digits exactly")]
        public string StaffMemberCNIC { get; set; }

        [Required, Display(Name = "Hiring Date"), DataType(DataType.DateTime)]
        public DateTime HiringDate { get; set; }

        [Required, Display(Name = "Email"), DataType(DataType.EmailAddress)]
        public string StaffMemberEmail { get; set; }

        [Required, Display(Name = "Date Of Birth"), DataType(DataType.DateTime)]
        public DateTime DateOfBirth { get; set; }

        [Required, Display(Name = "Designation")]
        public string Designation { get; set; }


        [Display(Name = "Qualification")]
        public string Qualification { get; set; }

        [Required,Display(Name = "Basic Salary")]
        public int Salary { get; set; }
    }
}