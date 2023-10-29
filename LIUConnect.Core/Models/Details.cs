using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LIUConnect.Core.Models
{
    public class Details
    {
        public int Id { get; set; } 
        public string? ProfilePicture { get; set; }  
        public string? Bio { get;set; }
        public string? Links { get; set; }
        public User User { get; set; }
    }
}
