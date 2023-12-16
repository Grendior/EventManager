using EventManager.DataAccess.Repository.IRepository;
using EventManager.Models;
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
            var events = _unitOfWork.Event.GetAll().ToList();

            return View(events);
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
    }
}
