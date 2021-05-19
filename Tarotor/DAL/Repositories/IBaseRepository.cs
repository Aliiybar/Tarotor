using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Tarotor.DAL.Repositories
{
    public interface IBaseRepository<T> where T : class
    {
        Task<T> GetAsync(string id);
        IQueryable<T> GetAll();
        IQueryable<T> FindBy(Expression<Func<T, bool>> predicate);
        void Add(T entity);
        void Delete(T entity);
        Task<int> UpdateAsync(T entity);
        Task<int> SaveAsync();
    }
}