using EventManager.DataAccess.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace EventManager.DataAccess.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _dbContext;
        internal DbSet<T> _dbSet;

        public Repository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<T>();
        }

        public void Add(T entity)
        {
            _dbSet.Add(entity);
        }

        public T? Get(Expression<Func<T, bool>> filters, string? includes = null)
        {
            IQueryable<T> query = _dbSet;
            query = query.Where(filters);
            if (!string.IsNullOrEmpty(includes))
            {
                foreach (var item in includes.Split(',', StringSplitOptions.RemoveEmptyEntries))     
                {
                    query = query.Include(item);
                }
            }
            return query.FirstOrDefault();
        }

        public IEnumerable<T> GetAll(string? includes = null)
        {
            IQueryable<T> query = _dbSet;
            if (!string.IsNullOrEmpty(includes))
            {
                foreach (var item in includes.Split(',', StringSplitOptions.RemoveEmptyEntries))     
                {
                    query = query.Include(item);
                }
            }
            return query.ToList();
        }

        
        public IEnumerable<T> GetAllFiltered(Expression<Func<T, bool>> filters, string? includes = null)
        {
            IQueryable<T> query = _dbSet;
            query = query.Where(filters);
            if (!string.IsNullOrEmpty(includes))
            {
                foreach (var item in includes.Split(',', StringSplitOptions.RemoveEmptyEntries))     
                {
                    query = query.Include(item);
                }
            }
            return query.ToList();
        }

        public void Remove(T entity)
        {
            _dbSet.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            _dbSet.RemoveRange(entities);

        }
    }
}
