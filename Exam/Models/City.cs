using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Exam.Models
{
    public class City
    {
        public int CityId { get; set; }
        public string CityName { get; set; }

        public int StateId { get; set; }
        // Navigation property for Country
        [ForeignKey("StateId")]

        
        public virtual State States { get; set; }
    }
}