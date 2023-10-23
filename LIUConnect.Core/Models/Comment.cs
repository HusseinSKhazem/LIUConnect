using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LIUConnect.Core.Models
{
    public class Comment
    {
        public int ID { get; set; } 
        public int UserID { get; set; }
        public DateTime dateTime { get; set; }  
        public string Content { get; set; }
        public User User { get; set; }        
    }
}
