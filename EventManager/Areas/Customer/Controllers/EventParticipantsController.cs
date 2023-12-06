using EventManager.DataAccess.Repository;
using EventManager.DataAccess.Repository.IRepository;
using EventManager.Models;
using EventManager.Utils;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EventManager.Areas.Customer.Controllers
{
    [Area(SD.Role_Customer)]
    public class EventParticipantsController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IUnitOfWork _unitOfWork;

        public EventParticipantsController(UserManager<IdentityUser> userManager, IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            var events = _unitOfWork.Event.GetAll().ToList();

            return View(events);
        }

        [HttpPost]
        public IActionResult SignIn(string eventId)
        {
            string userId = string.Empty;
            var user = _userManager.GetUserAsync(User).GetAwaiter().GetResult();
            if (user != null)
            {
                userId = user.Id;
            }

            if (!ModelState.IsValid)
            {
                return View();
            }

            var newParticipant = new EventParticipant();

            newParticipant.EventId = eventId;
            newParticipant.UserId = userId;
            newParticipant.Status = (int)Enums.AssignmentStatus.Applied;

            _unitOfWork.EventParticipant.Add(newParticipant);
            _unitOfWork.Save();
            TempData["success"] = "You have been added to an event";
            return RedirectToAction(nameof(Index));
        }
    }
}
