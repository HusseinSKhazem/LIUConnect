using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LIUConnect.Core.Models.Dtos
{
    public class VacancyDto
    {
        [Required]
        public string RecruiterEmail { get; set; }
        [Required]
        public string Status { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Requirements { get; set; }
        [Required]
        public int WorkingHours { get; set; }
        [Required]
        public string JobOffer { get; set; }
        [Required]
        public int MajorID { get; set; }
    }
}
