using EventManager.DataAccess;
using EventManager.Models;
using EventManager.Models.ViewModels;
using EventManager.Utils;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EventManager.Areas.Admin.Controllers
{
    [Area(SD.Role_Admin)]
    public class UserManagmentController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _dbContext;

        public UserManagmentController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, ApplicationDbContext dbContext)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            var users = _dbContext.ApplicationUsers.ToList();
            return View(users);
        }

        public IActionResult Upsert(string userId)
        {
            var user = _dbContext.ApplicationUsers.FirstOrDefault(x => x.Id == userId);
            var roleList = _roleManager.Roles.Where(x => x.Name != SD.Role_Admin).Select(x => x.Name)
                    .Select(i =>
                        new SelectListItem
                        {
                            Text = i,
                            Value = i
                        }
                    );

            if (user is null)
            {
                return View(new UserUpsertVM()
                {
                    RoleList = roleList
                });
            }



            return View(new UserUpsertVM()
            {
                ApplicationUser = user,
                Role = string.Join(", ", _userManager.GetRolesAsync(user).GetAwaiter().GetResult()),
                RoleList = roleList
            });
        }

        [HttpPost]
        public async Task<IActionResult> Upsert(UserUpsertVM model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            Activator.CreateInstance<ApplicationUser>();
            _dbContext.ApplicationUsers.Update(model.ApplicationUser);
            await _userManager.AddToRoleAsync(model.ApplicationUser, model.Role);
            _dbContext.SaveChanges();


            return RedirectToAction(nameof(Index));
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(string? userId)
        {
            if (userId is null)
            {
                return Content("Id is empty exist");
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user is null)
            {
                return Content("User doesn't exist");
            }

            var deleteResult = await _userManager.DeleteAsync(user);

            if (deleteResult.Succeeded)
            {
                return Content("User has been successfully deleted");
            }

            return Content("Something went wrong while deleting an user");
        }
    }
}
