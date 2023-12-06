namespace EventManager.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork
    {
        IEventRepository Event { get; }
        IEventParticipantsRepository EventParticipant { get; }

        void Save();
    }
}
