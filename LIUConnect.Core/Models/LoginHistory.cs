using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LIUConnect.Core.Models
{
    public class LoginHistory
    {
        public int Id { get; set; } 
        public string Email { get; set; }
        public DateTime dateTime { get; set; }
    }
}
