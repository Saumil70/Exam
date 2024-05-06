using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Exam.Models
{
    public class StudentEntites : DbContext
    {
        // Constructor
        public StudentEntites() : base("name=StudentEntites") 
        {
            
        }

        public DbSet<Student> Students {  get; set; }
        public DbSet<Countries> Countries { get; set; }

        public DbSet<State> States { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<ContactInfo> ContactInfos { get; set; }


    }
}