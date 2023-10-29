using LIUConnect.Core.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace LIUConnect.EF.Repository
{
    public class AuthRepository : IAuthRepository
    {
        //Creating Hashed Password Section
        public void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));  
            }
        }

        //Getting Role from the int
        public string GetRoleFromUserRole(int UserRole)
        {
            switch(UserRole)
            {
                case 0: return "Admin";
                case 1: return "Instructor";
                case 2: return "Recruiter";
                case 3: return "Student";
                default: return string.Empty;
            }
        }
        
        //Verifying the pasword used in the log in process
        public bool verifyPassword(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++) 
                {
                    if (computedHash[i] != passwordHash[i]) return false;
                }
            }
            return true;
        }

    }
}
