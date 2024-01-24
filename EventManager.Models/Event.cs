using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventManager.Models
{
    public class Event
    {
        [Key]
        public string? Id { get; set; }
        [Required]
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateTime Date { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int? Capacity { get; set; }
        public int? Occupied { get; set; } = 0;
        public string? CreatorId { get; set; }
        [ForeignKey(nameof(CreatorId))]
        [ValidateNever]
        public ApplicationUser? Creator { get; set; }

        public string? ImageUrl { get; set; }
    }
}
