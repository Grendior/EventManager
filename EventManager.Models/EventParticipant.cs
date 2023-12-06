using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

        public int Capacity { get; set; }

        public int Status { get; set; }
    }
}
