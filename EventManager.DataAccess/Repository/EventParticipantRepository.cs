using EventManager.DataAccess.Repository.IRepository;
using EventManager.Models;

namespace EventManager.DataAccess.Repository
{
    public class EventParticipantRepository(ApplicationDbContext dbContext) : Repository<EventParticipant>(dbContext), IEventParticipantsRepository
    {
        private readonly ApplicationDbContext _dbContext = dbContext;

        public void Update(EventParticipant entity)
        {
            _dbContext.EventParticipants.Update(entity);
        }
    }
}
