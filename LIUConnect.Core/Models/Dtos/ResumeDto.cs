using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LIUConnect.Core.Models.Dtos
{
    public class ResumeDto
    {
        [Required]
        public string location { get; set; }
        [Required]
        public string Socials { get; set; }
        [Required]
        public string projects { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public string EducationalBackground { get; set; }
        [Required]
        public string WorkExperience { get; set; }
        [Required]
        public string Skills { get; set; }
    }
}
