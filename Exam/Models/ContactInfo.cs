using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Exam.Models
{

        public class ContactInfo
        {
            [Key]
            public int ContactInfoId { get; set; }

            [Required(ErrorMessage = "Email is required")]
            [EmailAddress(ErrorMessage = "Invalid Email Address")]
            public string Email { get; set; }

            [Required(ErrorMessage = "Phone number is required")]
            [RegularExpression(@"^\d{10}$", ErrorMessage = "Phone number must be 10 digits")]
            public string PhoneNumber { get; set; }


        }



    
}