using EventManager.Models;

namespace EventManager.DataAccess.Repository.IRepository
{
    public interface IEventParticipantsRepository : IRepository<EventParticipant>
    {
        void Update(EventParticipant entity);
    }
}
