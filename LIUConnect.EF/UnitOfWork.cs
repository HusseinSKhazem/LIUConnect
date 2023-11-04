using LIUConnect.Core;
using LIUConnect.Core.Interface;
using LIUConnect.Core.Models;
using LIUConnect.EF.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LIUConnect.EF
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public IBaseRepository<Recruiter> Recruiters { get; private set; }
        public IBaseRepository<Instructor> Instructors { get; private set; }
        public IBaseRepository<Student> Students { get; private set; }
        public IBaseRepository<User> Users { get; private set; }
        public IBaseRepository<Admin> Admins { get; private set; }
        public IBaseRepository<Major> Majors { get; private set; }
        public IBaseRepository<Details> Details { get; private set; }
        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Recruiters = new BaseRepository<Recruiter>(_context);
            Instructors = new BaseRepository<Instructor>(_context);
            Students = new BaseRepository<Student>(_context);
            Users = new BaseRepository<User>(_context);
            Admins = new BaseRepository<Admin>(_context);
            Majors = new BaseRepository<Major>(_context);
            Details = new BaseRepository<Details>(_context);
        }

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
