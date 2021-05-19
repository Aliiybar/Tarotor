using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Tarotor.DAL.Repositories
{

    public abstract class BaseRepository<T> :
        IBaseRepository<T> where T : class
    {
        private readonly AppDbContext _entities;

        protected BaseRepository(AppDbContext appDbContext)
        {
            Context = appDbContext;
            _entities = appDbContext;
        }

        public AppDbContext Context { get; }

        public virtual async Task<T> GetAsync(string id)
        {
            T entity = await _entities.Set<T>().FindAsync(id);
            return entity;
        }

   
        public virtual IQueryable<T> GetAll()
        {
            IQueryable<T> query = _entities.Set<T>();
            return query;
        }

        public IQueryable<T> FindBy(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {
            IQueryable<T> query = _entities.Set<T>().Where(predicate);
            return query;
        }

        public virtual void Add(T entity)
        {
            _entities.Set<T>().Add(entity);
        }

        public virtual void Delete(T entity)
        {
            _entities.Set<T>().Remove(entity);
        }

        public async Task<int>  UpdateAsync(T entity)
        {
            _entities.Entry(entity).State = EntityState.Modified;
            _entities.Update(entity);
            return await _entities.SaveChangesAsync();

        }

        public virtual async Task<int> SaveAsync()
        {
            return await _entities.SaveChangesAsync();
        }
    }
  
}