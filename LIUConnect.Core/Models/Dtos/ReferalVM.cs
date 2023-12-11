using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LIUConnect.Core.Models.Dtos
{
    public class ReferalVM
    {
        [Required]
        public string StudentEmail {  get; set; }
        [Required]
        public string InstructorEmail { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public int vacancyID { get; set; }  

    }
}
