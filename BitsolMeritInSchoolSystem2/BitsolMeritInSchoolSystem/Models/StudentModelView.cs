using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BitsolMeritInSchoolSystem.Models
{
    public class StudentModelView
    {

        public int StudentId { get; set; }
        [Required, Display(Name = "Student Name")]
        public string StudentName { get; set; }

        [Required, Display(Name = "Father Name")]
        public string FatherName { get; set; }

        [Required, Display(Name = "Contact No"), DataType(DataType.PhoneNumber), MaxLength(14), MinLength(11)]
        public string ContactNo { get; set; }

        [Required, Display(Name = "Bay-Form No"), MinLength(13, ErrorMessage = "CNIC must have 13 digits exactly"), MaxLength(13, ErrorMessage = "CNIC must have 13 digits exactly")]
        public string BayFormNo { get; set; }

        [Required, Display(Name = "Registration Date"), DataType(DataType.DateTime)]
        public DateTime Date { get; set; }

        [Required, Display(Name = "Email"), RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$", ErrorMessage = "Incorrect Email Address")]
        public string Email { get; set; }

        [Required, Display(Name = "Date Of Birth"), DataType(DataType.DateTime)]
        public DateTime DateOfBirth { get; set; }

      

        [Required, Display(Name = "Class Section")]
        public int ClassSectionId { get; set; }
        public ClassSection ClassSection { get; set; }

        public bool Closed { get; set; }
        public bool Update { get; set; }



        [Display(Name = "Student Image")]
        //public byte[] ProductImage { get; set; }
        public string StudentImage { get; set; }

    }
}