using LIUConnect.Core.Models;
using LIUConnect.Core.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LIUConnect.Core.Interface
{
    public interface IUserAuthentication:IAuthRepository
    {
        Task<Recruiter> Register(Register dto);
        Task<String> Login(UserDto dto);
        Task<bool> ClientExists(string username);
        Task<Admin> AddAdmin(adminRegister dto);
        Task<Instructor> AddInstructor(Register dto);
        Task<Student> AddStudent(StudentVM dto);
    }
}
