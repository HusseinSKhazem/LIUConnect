using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LIUConnect.Core.Models.Dtos
{
    public class RecommnedationDto
    {
        [Required]
        public string InstructorEmail {  get; set; } = string.Empty;
        [Required]
        public string StudentEmail {  get; set; } = string.Empty;
        [Required]
        public string description { get; set; } = string.Empty;
    }
}
