using EventManager.DataAccess.Repository.IRepository;
using EventManager.Models;

namespace EventManager.DataAccess.Repository
{
    public class EventRepository(ApplicationDbContext dbContext) : Repository<Event>(dbContext), IEventRepository
    {
        private readonly ApplicationDbContext _dbContext = dbContext;


        public void Update(Event entity)
        {
            _dbContext.Events.Update(entity);
        }
    }
}
