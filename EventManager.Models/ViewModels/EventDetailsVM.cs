namespace EventManager.Models.ViewModels
{
    public class EventDetailsVM
    {
        public required Event Event { get; set; }

        public List<EventParticipant>? Participants { get; set; }
    }
}
