using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LIUConnect.Core.Models
{
    public class Resume
    {
        public int ResumeID { get; set; }
      
        public string? location { get; set; }
        public string? Socials { get; set; }
        public string? projects { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; } 
        public string? Email { get; set; } 
        public string? PhoneNumber { get; set; } 
        public string? EducationalBackground { get; set; } 
        public string? WorkExperience { get; set; } 
        public string? Skills { get; set; }
        [ForeignKey("Student")]
        public int StudentID { get; set; }
        public Student Student { get; set; }
    }

}
