using System.Linq.Expressions;

namespace EventManager.DataAccess.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        IEnumerable<T> GetAllFiltered(Expression<Func<T, bool>> filters);
        T? Get(Expression<Func<T, bool>> filters);
        void Add(T entity);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entities);
    }
}
