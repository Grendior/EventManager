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
                return Content(UserNotFound, MediaTypeNames.Text.Plain);
            }

            var selectedEvent = _unitOfWork.Event.Get(e => e.Id == eventId);
            if (selectedEvent is null)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Content(EventNotFound, MediaTypeNames.Text.Plain);
            }

            var existingParticipant = _unitOfWork.EventParticipant.Get(ep => ep.EventId == eventId && ep.UserId == user.Id);
            if (existingParticipant is not null)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Content(EventParticipantsAlreadySigned, MediaTypeNames.Text.Plain);
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
                return Content(EventParticipantsError, MediaTypeNames.Text.Plain);
            }

            return Content(EventParticipantsSigned, MediaTypeNames.Text.Plain);
        }

        [HttpDelete]
        public IActionResult SignOut(string? userId)
        {
            var participant = _unitOfWork.EventParticipant.Get(x => x.UserId == userId);
            if(participant is null) 
            {
                return Content(EventParticipantsNotSignedUp, MediaTypeNames.Text.Plain);
            }

            _unitOfWork.EventParticipant.Remove(participant);
            _unitOfWork.Save();

            return Content(EventParticipantsSignedOut, MediaTypeNames.Text.Plain);
        }
    }
}
