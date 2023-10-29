using LIUConnect.Core;
using LIUConnect.Core.Interface;
using LIUConnect.Core.Models;
using LIUConnect.Core.Models.Dtos;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace LIUConnect.EF.Repository
{
    public class UserAuthentication : AuthRepository , IUserAuthentication
    {
        private readonly IUnitOfWork _unitOfWork;
        private IConfiguration _configuration;

        public UserAuthentication(IUnitOfWork unitOfWork, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;   
            _configuration = configuration; 
        }

        //<-- Add Admin Section--!>
        public async Task<Admin> AddAdmin(Register dto)
        {
            try
            {
                if (await ClientExists(dto.Username))
                {
                    throw new Exception("Username is already taken");
                }

                byte[] passwordHash, passwordSalt;
                CreatePasswordHash(dto.Password, out passwordHash, out passwordSalt);

                var user = await _unitOfWork.Users.CreateClientAsync(dto);
                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;
                user.Email = dto.Email;
                user.UserRole = 0; 

                var admin = new Admin
                {
                    User = user
                };

                await _unitOfWork.Admins.AddAsync(admin);
                await _unitOfWork.SaveAsync();

              
                return admin;
            }
            catch (Exception ex)
            {
               
                Console.WriteLine($"Error during registration: {ex.Message}");

               
                throw;
            }
        }
        //<-- End Add Admin Section--!>


        //<--Add Instructor Section--!>
        public async Task<Instructor> AddInstructor(Register dto)
        {
            try
            {
                if (await ClientExists(dto.Username))
                {
                    throw new Exception("Username is already taken");
                }

                byte[] passwordHash, passwordSalt;
                CreatePasswordHash(dto.Password, out passwordHash, out passwordSalt);

                var user = await _unitOfWork.Users.CreateClientAsync(dto);
                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;
                user.Email = dto.Email;
                user.UserRole = 1;

                var instructor = new Instructor
                {
                    User = user
                };

                await _unitOfWork.Instructors.AddAsync(instructor);
                await _unitOfWork.SaveAsync();


                return instructor;
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error during registration: {ex.Message}");


                throw;
            }
        }
        //<--End Add Instructor Section--!>

        //<--Add Student Section--!>
        public async Task<Student> AddStudent(StudentVM dto)
        {
            try
            {
                if (await ClientExists(dto.Username))
                {
                    throw new Exception("Username is already taken");
                }

                byte[] passwordHash, passwordSalt;
                CreatePasswordHash(dto.Password, out passwordHash, out passwordSalt);

                var user = await _unitOfWork.Users.CreateClientAsync(dto);
                var major = _unitOfWork.Majors.GetById(dto.MajorID);

                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;
                user.Email = dto.Email;
                user.UserRole = 3;

                var student = new Student
                {
                    User = user,
                    MajorID = dto.MajorID,
                    Major = major
                };

                await _unitOfWork.Students.AddAsync(student);
                await _unitOfWork.SaveAsync();


                return student;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during registration: {ex.Message}");

                // Log the exception details using your preferred logging mechanism (e.g., ILogger)
                Console.WriteLine($"Exception: {ex}");

                // If there's an inner exception, print or log its details
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"Inner Exception: {ex.InnerException.Message}");
                    Console.WriteLine($"Inner Exception Stack Trace: {ex.InnerException.StackTrace}");
                }

                throw;
            }
        }
        
        //<-- End Add Student Section--!>


        public async Task<bool> ClientExists(string username)
        {
            if (await _unitOfWork.Users.GetFirstOrDefaultAsync(x => x.Username == username) != null)
                return true;
            return false;
        }



        public async Task<string> Login(UserDto dto)
        {
            var user = await _unitOfWork.Users.GetFirstOrDefaultAsync(x => x.Username == dto.Username);

            if (user == null || !verifyPassword(dto.Password, user.PasswordHash, user.PasswordSalt))
                throw new Exception("incorrect email or password");

            var token = CreateToken(user);
            return token;
        }





        //Register Section (Recruiters)
        public async Task<Recruiter> Register(Register dto)
        {
            try
            {
                if (await ClientExists(dto.Username))
                {
                    throw new Exception("Username is already taken");
                }

                byte[] passwordHash, passwordSalt;
                CreatePasswordHash(dto.Password, out passwordHash, out passwordSalt);

                var user = await _unitOfWork.Users.CreateClientAsync(dto);
                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;
                user.Email = dto.Email; 
                user.UserRole = 2; 

                var recruiter = new Recruiter
                {
                    User = user
                };

                await _unitOfWork.Recruiters.AddAsync(recruiter);
                await _unitOfWork.SaveAsync();

                return recruiter;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during registration: {ex.Message}");
                throw;
            }
        }
         private string CreateToken(User client)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name , client.Username),
                new Claim(ClaimTypes.Email , client.Email),
                new Claim(ClaimTypes.Role , GetRoleFromUserRole(client.UserRole))
            };
            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                _configuration.GetSection("JWT:Token").Value!));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }
        private string GetRoleFromUserRole(int UserRole)
        {
            switch (UserRole)
            {
                case 0: return "Admin";
                case 1: return "Instructor";
                case 2: return "Recruiter";
                case 3: return "Student";
                default: return string.Empty;
            }
        }

    }

}

    

