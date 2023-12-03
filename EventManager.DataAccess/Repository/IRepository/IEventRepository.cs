using EventManager.Models;

namespace EventManager.DataAccess.Repository.IRepository
{
    public interface IEventRepository : IRepository<Event>
    {
        void Update(Event entity);
    }
}
