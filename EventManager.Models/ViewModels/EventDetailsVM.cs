namespace EventManager.Models.ViewModels
{
    public class EventDetailsVM
    {
        public required Event Event { get; init; }
        
        public required bool IsFull { get; set; } 

        public List<EventParticipant>? Participants { get; set; }
    }
}
