using EventManager.DataAccess.Repository;
using EventManager.DataAccess.Repository.IRepository;
using EventManager.Models;
using EventManager.Utils;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using static EventManager.Utils.Enums;
using static EventManager.Utils.TempDataInfos;

namespace EventManager.Areas.Customer.Controllers
{
    [Area(SD.Role_Customer)]
    public class EventParticipantsController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<EventParticipantsController> _logger;

        public EventParticipantsController(UserManager<IdentityUser> userManager, IUnitOfWork unitOfWork, ILogger<EventParticipantsController> logger)
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public IActionResult Index()
        {
            var events = _unitOfWork.Event.GetAll().ToList();

            return View(events);
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(string eventId)
        {
            string userId = string.Empty;
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                TempData["error"] = UserNotFound;
                return RedirectToAction(nameof(Index));
            }

            var selectedEvent = _unitOfWork.Event.Get(e => e.Id == eventId);
            if (selectedEvent == null)
            {
                TempData["error"] = EventNotFound;
                return RedirectToAction(nameof(Index));
            }

            var existingParticipant = _unitOfWork.EventParticipant.Get(ep => ep.EventId == eventId && ep.UserId == userId);
            if (existingParticipant != null)
            {
                TempData["error"] = EventParticipantsAlreadySigned;
                return RedirectToAction(nameof(Index));
            }

            var newParticipant = new EventParticipant
            {
                EventId = eventId,
                UserId = user.Id,
                Status = AssignmentStatus.Applied
            };

            try
            {
                _unitOfWork.EventParticipant.Add(newParticipant);
                _unitOfWork.Save();
                TempData["success"] = EventParticipantsSigned;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception");
                TempData["error"] = EventParticipantsError;
                return RedirectToAction(nameof(Index));
            }
            
            return RedirectToAction(nameof(Index));
        }
    }
}
