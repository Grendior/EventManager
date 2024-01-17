using EventManager.DataAccess.Repository.IRepository;
using EventManager.Models;
using EventManager.Models.ViewModels;
using EventManager.Utils;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Mime;
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
            var eventsVM = new EventsVM
            {
                UserId = _userManager.GetUserId(User),
                Events = _unitOfWork.Event.GetAll().ToList(),
                EventParticipants = _unitOfWork.EventParticipant.GetAll().ToList()
            };

            return View(eventsVM);
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(string eventId)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user is null)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new { success = false, message = UserNotFound });
            }

            var selectedEvent = _unitOfWork.Event.Get(e => e.Id == eventId);
            if (selectedEvent is null)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new { success = false, message = EventNotFound });
            }

            var existingParticipant = _unitOfWork.EventParticipant.Get(ep => ep.EventId == eventId && ep.UserId == user.Id);
            if (existingParticipant is not null)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new { success = false, message = EventParticipantsAlreadySigned });
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
                _unitOfWork.Event.Update(selectedEvent);
                _unitOfWork.Save();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception");
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new { success = false, message = EventParticipantsError });
            }

            return Json(new { success = true, message = EventParticipantsSigned });
        }

        [HttpDelete]
        public IActionResult SignOut(string? userId, string? eventId)
        {
            var participant = _unitOfWork.EventParticipant.Get(x => x.UserId == userId && x.EventId == eventId);
            if(participant is null) 
            {
                return Json(new { success = false, message = EventParticipantsNotSignedUp });
            }

            _unitOfWork.EventParticipant.Remove(participant);
            _unitOfWork.Save();

            return Json(new { success = false, message = EventParticipantsSignedOut });
        }
    }
}
