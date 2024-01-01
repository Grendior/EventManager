using EventManager.DataAccess;
using EventManager.DataAccess.Repository.IRepository;
using EventManager.Models;
using EventManager.Models.ViewModels;
using EventManager.Utils;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;
using System.Text.Encodings.Web;

namespace EventManager.Areas.Admin.Controllers
{
    [Area(SD.Role_Admin)]
    public class UserManagmentController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserStore<IdentityUser> _userStore;
        private readonly IUserEmailStore<IdentityUser> _emailStore;
        private readonly IEmailSender _emailSender;
        private readonly ApplicationDbContext _dbContext;


        public UserManagmentController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, IUnitOfWork unitOfWork, IUserStore<IdentityUser> userStore, IEmailSender emailSender, ApplicationDbContext dbContext)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _userStore = userStore;
            _unitOfWork = unitOfWork;
            _emailSender = emailSender;
            _emailStore = GetEmailStore();
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
            var roleList = _roleManager.Roles.Select(x => x.Name)
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

            var user = _dbContext.ApplicationUsers.FirstOrDefault(x => x.Id == model.ApplicationUser.Id);
            var update = true;
            if (user is null)
            {
                user = Activator.CreateInstance<ApplicationUser>();
                update = false;
            }

            user.FirstName = model.ApplicationUser.FirstName;
            user.LastName = model.ApplicationUser.LastName;
            user.Email = model.ApplicationUser.Email;

            await _userStore.SetUserNameAsync(user, model.ApplicationUser.Email, CancellationToken.None);
            await _emailStore.SetEmailAsync(user, model.ApplicationUser.Email, CancellationToken.None);

            IdentityResult result;
            if (update)
            {
                result = await _userManager.UpdateAsync(user);
            }
            else
            {
                var password = CreateRandomPassword();
                result = await _userManager.CreateAsync(user, password);

                if (user == null || string.IsNullOrEmpty(user.Email))
                {
                    return Content(TempDataInfos.SendingInformationUserOrEventNotFound);
                }

                var code = await _userManager.GeneratePasswordResetTokenAsync(user);
                code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                var callbackUrl = Url.Page(
                    "/Account/ResetPassword",
                    pageHandler: null,
                    values: new { area = "Identity", code },
                    protocol: Request.Scheme);

                if (callbackUrl == null)
                {
                    return Content(TempDataInfos.SendingInformationInvalidLink);
                }

                await _emailSender.SendEmailAsync(
                    user.Email,
                    "New account create",
                    $"Your account have been prepered by admin of the website. <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>Click here to change password</a>.");


            }

            if (result.Succeeded)
            {
                var role = _userManager.GetRolesAsync(user).GetAwaiter().GetResult().SingleOrDefault();
                if (role is not null)
                {
                    await _userManager.RemoveFromRoleAsync(user, role);
                }
                if (role != model.Role!)
                {
                    await _userManager.AddToRoleAsync(user, model.Role ?? SD.Role_Customer);
                }

                return RedirectToAction(nameof(Index));
            }
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return View(model);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(string? userId)
        {
            if (userId is null)
            {
                return Json(new { success = false, message = "userId is empty exist" });
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user is null)
            {
                return Json(new { success = false, message = "User doesn't exist" });
            }

            var listOfUserParticipation = _unitOfWork.EventParticipant.GetAllFiltered(x => x.UserId == userId);
            if (listOfUserParticipation.Any())
            {
                _unitOfWork.EventParticipant.RemoveRange(listOfUserParticipation);
            }

            var deleteResult = await _userManager.DeleteAsync(user);

            if (deleteResult.Succeeded)
            {
                _dbContext.SaveChanges();
                return Json(new { success = true, message = "User has been successfully deleted" });
            }

            return Json(new { success = false, message = "Something went wrong while deleting an user" });
        }

        private IUserEmailStore<IdentityUser> GetEmailStore()
        {
            if (!_userManager.SupportsUserEmail)
            {
                throw new NotSupportedException("The default UI requires a user store with email support.");
            }
            return (IUserEmailStore<IdentityUser>)_userStore;
        }

        private static string CreateRandomPassword(int length = 15)
        {
            var smallLetters = "abcdefghijklmnopqrstuvwxyz";
            var bigLetters = "ABCDEFGHJKLMNOPQRSTUVWXYZ";
            var number = "0123456789";
            var nonAlphanumeric = "!@#$%^&*?_-";
            string validChars = bigLetters + smallLetters + number + nonAlphanumeric;
            Random random = new Random();

            char[] chars = new char[length];
            for (int i = 0; i < length; i++)
            {
                if (!chars.Any(smallLetters.Contains))
                {
                    chars[i] = smallLetters[random.Next(0, smallLetters.Length)];
                    continue;
                }

                if (!chars.Any(bigLetters.Contains))
                {
                    chars[i] = bigLetters[random.Next(0, bigLetters.Length)];
                    continue;
                }

                if (!chars.Any(number.Contains))
                {
                    chars[i] = number[random.Next(0, number.Length)];
                    continue;
                }

                if (!chars.Any(nonAlphanumeric.Contains))
                {
                    chars[i] = nonAlphanumeric[random.Next(0, nonAlphanumeric.Length)];
                    continue;
                }

                chars[i] = validChars[random.Next(0, validChars.Length)];
            }

            return new string(chars);
        }
    }
}
