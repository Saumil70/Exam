using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Exam.Models
{
    public class State
    {
        [Key]
        public int StateId { get; set; }
        public string StateName { get; set; }

        public int CountryId { get; set; }

        // Navigation property for Country
        [ForeignKey("CountryId")]
        public virtual Countries Country { get; set; }
    }
}