using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Policy;
using System.Web;

namespace Exam.Models
{
    public class Student
    {
        public int StudentId { get; set; }
        [Required(ErrorMessage = "First Name is required")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Date of Birth is required")]
        [Display(Name = "Date of Birth")]
        [DataType(DataType.Date)]
        /*[RegularExpression(@"^\d{2}-\d{2}-\d{4}$", ErrorMessage = "Date of Birth format should be DD-MM-YYYY")]*/
        public DateTime DOB { get; set; }

        [Required(ErrorMessage = "Country is required")]
        public int CountryId { get; set; }

        [Required(ErrorMessage = "State is required")]
        public int StateId { get; set; }

        [Required(ErrorMessage = "City is required")]
        public int CityId { get; set; }

        [Required(ErrorMessage = "Salary is required")]
        [Range(0, double.MaxValue, ErrorMessage = "Salary must be a positive number")]
        public decimal Salary { get; set; }

        [Required(ErrorMessage = "Starting date is required")]
        [DataType(DataType.Date)]
        [Display(Name = "Starting Date")]
        public DateTime StartingDate { get; set; }

        [Required(ErrorMessage = "Position is required")]
        public string Position { get; set; }
        public bool IsActive { get; set; }

        public int ContactInfoId { get; set; }

        [ForeignKey("ContactInfoId")]

        public virtual ContactInfo ContactInfo { get; set; }

        // Navigation property for Country
        [ForeignKey("CountryId")]
      
        public virtual Countries Country { get; set; }

        // Navigation property for State
        [ForeignKey("StateId")]
     
        public virtual State States { get; set; }

        // Navigation property for City
        [ForeignKey("CityId")]
        
        public virtual City Cities { get; set; }







    }
}