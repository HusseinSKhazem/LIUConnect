using LIUConnect.Core.Interface;
using LIUConnect.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LIUConnect.Core
{
    public interface IUnitOfWork : IDisposable
    {
        IBaseRepository<Recruiter> Recruiters { get; }
        IBaseRepository<Instructor> Instructors { get; }
        IBaseRepository<Student> Students { get; }
        IBaseRepository<User> Users { get; }
        IBaseRepository<Admin> Admins { get; }
        IBaseRepository<Major> Majors { get; }
        int Complete();
        Task SaveAsync();
    }
}
