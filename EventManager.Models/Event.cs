using System.ComponentModel.DataAnnotations;

namespace EventManager.Models
{
    public class Event
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int Capacity { get; set; }

        //public Guid? CreatorId { get; set; }
        //[ForeignKey(nameof(CreatorId))]
        //public User Creator { get; set; }
    }
}
