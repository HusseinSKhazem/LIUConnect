using LIUConnect.Core.Interface;
using LIUConnect.Core.Models;
using LIUConnect.Core.Models.Dtos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LIUConnect.EF.Repository
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected ApplicationDbContext _context;
        public BaseRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(T entity)
        {
           await _context.Set<T>().AddAsync(entity);
        }
        public void Attach(T entity)
        {
            _context.Set<T>().Attach(entity);   
        }

        public async Task<User> CreateClientAsync(Register dto)
        {
            var user = new User
            {
                Username = dto.Username
            };
            return user;
        }

        public async Task<User> CreateClientAsync(StudentVM dto)
        {
            var user = new User
            {
                Username = dto.Username
            };
            return user;
        }

        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public T Find(Expression<Func<T, bool>> criteria, string[] includes = null)
        {
            IQueryable<T> query = _context.Set<T>();
            if (includes != null)
                foreach (var incluse in includes)
                    query = query.Include(incluse);
            return query.SingleOrDefault(criteria);
        }

        public IEnumerable<T> FindAll(Expression<Func<T, bool>> criteria, string[] includes = null)
        {
            IQueryable<T> query = _context.Set<T>();
            if (includes != null)
                foreach (var incluse in includes)
                    query = query.Include(incluse);
            return query.ToList();
        }

        public IEnumerable<T> GetAll()
        {
            return _context.Set<T>().ToList();
        }

        public T GetById(int id)
        {
            return _context.Set<T>().Find(id); 
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<T> GetFirstOrDefaultAsync(Expression<Func<T, bool>> filter, string[] includes = null)
        {
            IQueryable<T> query = _context.Set<T>();
            if (includes != null)
                foreach (var incluse in includes)
                    query = query.Include(incluse);
            return await query.FirstOrDefaultAsync(filter);
        }

        public T Update(T entity)
        {
            _context.Update(entity);
            return entity;
        }
    }
}
