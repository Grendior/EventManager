using EventManager.DataAccess.Repository.IRepository;

namespace EventManager.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext _dbContext;
        public IEventRepository Event { get; private set; }
        public IEventParticipantsRepository EventParticipant { get; private set; }
        public UnitOfWork(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            Event = new EventRepository(dbContext);
            EventParticipant = new EventParticipantRepository(dbContext);
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }
    }
}
