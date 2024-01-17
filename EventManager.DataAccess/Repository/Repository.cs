using EventManager.DataAccess.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace EventManager.DataAccess.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly DbSet<T> _dbSet;

        protected Repository(ApplicationDbContext dbContext)
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
            if (string.IsNullOrEmpty(includes))
            {
                return query.FirstOrDefault();
            }

            query = includes.Split(',', StringSplitOptions.RemoveEmptyEntries)
                .Aggregate(query, (current, item) => current.Include(item));

            return query.FirstOrDefault();
        }

        public IEnumerable<T> GetAll(string? includes = null)
        {
            IQueryable<T> query = _dbSet;
            if (string.IsNullOrEmpty(includes))
            {
                return query.ToList();
            }

            query = includes.Split(',', StringSplitOptions.RemoveEmptyEntries)
                .Aggregate(query, (current, item) => current.Include(item));

            return query.ToList();
        }


        public IEnumerable<T> GetAllFiltered(Expression<Func<T, bool>> filters, string? includes = null)
        {
            IQueryable<T> query = _dbSet;
            query = query.Where(filters);
            if (string.IsNullOrEmpty(includes))
            {
                return query.ToList();
            }

            query = includes.Split(',', StringSplitOptions.RemoveEmptyEntries)
                .Aggregate(query, (current, item) => current.Include(item));

            return query.ToList();
        }

        public long GetAllFilteredCount(Expression<Func<T, bool>> filters)
        {
            IQueryable<T> query = _dbSet;

            return query.Count(filters);
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