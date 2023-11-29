using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LIUConnect.Core.Models.Dtos
{
    public class CommentDto
    {
        [Required]
        public int VacancyId { get; set; }
        [Required]
        public string content { get; set;} = string.Empty;
        [Required]
        public string UserEmail { get; set;} = string.Empty;    
    }
}
