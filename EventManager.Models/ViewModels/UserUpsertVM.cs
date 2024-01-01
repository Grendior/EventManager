using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace EventManager.Models.ViewModels
{
    public class UserUpsertVM
    {
        public ApplicationUser ApplicationUser { get; set; } = new ApplicationUser();
        [Required]
        public string? Role { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem>? RoleList { get; set; }
    }
}
