using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LIUConnect.Core.Models
{
    //User Model
    public class User
    {
        public int UserId { get; set; }
        public string Email { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string Username { get; set; } = string.Empty;
        public List<Comment>? Comments { get; set; }
        [Range(0, 3,ErrorMessage ="Values must be between 0 and 3")]    
        public int UserRole {  get; set; }      
        
    }
}




