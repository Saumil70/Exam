using Exam.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;
using System.Web.Mvc;

namespace Exam.StudentViewModel
{
    public class StudentViewModel
    {

        [Required]
        public int StudentId { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public DateTime DOB { get; set; }
        [Required]
        public int CountryId { get; set; }
        [Required]

        public int StateId { get; set; }
        [Required]
        public int CityId { get; set; }
        [Required]
        public decimal Salary { get; set; }
        [Required]
        public DateTime StartingDate { get; set; }
        [Required]
        public string Position { get; set; }
        [Required]
        public bool IsActive { get; set; }
        [Required]
        public int ContactInfoId { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string PhoneNumber { get; set; }

    
        public virtual Countries Countries { get; set; }
  
        public virtual State State { get; set; }
      
        public virtual City City { get; set; }

        public string CountryName {  get; set; }
        public string StateName { get; set; }
        public string CityName {  get; set; }         

        /*        public IEnumerable<SelectListItem> CountryList { get; set; }
                public IEnumerable<SelectListItem> StateList { get; set; }
                public IEnumerable<SelectListItem> CityList { get; set; }*/

    }
}