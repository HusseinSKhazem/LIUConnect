using LIUConnect.Core.Models;
using LIUConnect.Core.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LIUConnect.Core.Interface
{
    public interface IBaseRepository<T> where T : class
    {
        T GetById(int id);
        Task AddAsync(T entity);
        Task<T> GetByIdAsync(int id);  
        IEnumerable<T> GetAll();
        T Find(Expression<Func<T, bool>> criteria, string[] includes=null);
        IEnumerable<T> FindAll(Expression<Func<T, bool>> criteria, string[]  includes=null);
        T Update(T entity);
        void Delete(T entity);
        void Attach(T entity);
        Task<T> GetFirstOrDefaultAsync(Expression<Func<T, bool>> filter, string[] includes = null);
        Task<User> CreateClientAsync(Register dto);
        Task<User> CreateClientAsync(StudentVM dto);
        Task<User> CreateClientAsync(InstructorVM dto);
        Task<User> CreateClientAsync(adminRegister dto);
    }
}
