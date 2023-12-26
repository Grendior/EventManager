using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EventManager.Models.ViewModels
{
    public class UserUpsertVM
    {
        public ApplicationUser ApplicationUser { get; set; } = new ApplicationUser();
        public string? Role { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem>? RoleList { get; set; }
    }
}
