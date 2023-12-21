namespace EventManager.Models.ViewModels
{
    public class EventsVM
    {
        public string? UserId { get; set; }
        public List<Event> Events { get; set; } = [];
        public List<EventParticipant> EventParticipants { get; set; } = [];
    }
}
