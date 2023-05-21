using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Repositories.Interface
{
    public interface IRepository<T> where T : class
    {
        Task<List<T>> GetAllAsync();
        Task<T> GetByAsync(Expression<Func<T, bool>> predicate);
        Task<List<T>> FindByAsync(Expression<Func<T, bool>> predicate);
        Task<int> CountAsync();
    }
}