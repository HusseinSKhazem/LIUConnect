using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LIUConnect.Core.Models.Dtos
{
    public class Register
    {
        [Required]
        public string Username {  get; set; } = string.Empty;
        [Required]
        public string Password { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string location { get; set; }
        [Required]
        public string website { get; set; }
        [Required]
        public string companyName { get; set; }

    }
}
