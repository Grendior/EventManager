using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;
using static EventManager.Utils.Enums;

namespace EventManager.Models
{
    public class EventParticipant
    {
        public int Id { get; set; }

        public string? EventId { get; set; }
        [ForeignKey(nameof(EventId))]
        [ValidateNever]
        public Event? Event { get; set; }

        public string? UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        [ValidateNever]
        public ApplicationUser? User { get; set; }

        public AssignmentStatus Status { get; set; }
    }
}
