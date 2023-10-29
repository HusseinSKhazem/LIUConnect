using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LIUConnect.Core.Interface
{
    public interface IAuthRepository
    {
        bool verifyPassword(string password, byte[] passwordHash, byte[] passwordSalt);
        void CreatePasswordHash(string password,out byte[] passwordHash,out byte[] passwordSalt);
        string GetRoleFromUserRole(int UserRole);
    }
}
