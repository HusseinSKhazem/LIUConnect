using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LIUConnect.Core.Models.Dtos
{
    public class ApplicationDto
    {
        [Required]
        public int VacancyId { get; set; }
        [Required]
        public IFormFile CvFile { get; set; }
    }
}
