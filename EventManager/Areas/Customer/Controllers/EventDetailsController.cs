using EventManager.DataAccess;
using EventManager.DataAccess.Repository.IRepository;
using EventManager.Models.ViewModels;
using EventManager.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static EventManager.Utils.Enums;

namespace EventManager.Areas.Customer.Controllers
{
    [Area(SD.Role_Customer)]
    public class EventDetailsController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEmailSender _emailSender;
        private readonly ILogger<EventParticipantsController> _logger;

        public EventDetailsController(ApplicationDbContext dbContext, UserManager<IdentityUser> userManager, IUnitOfWork unitOfWork, IEmailSender emailSender, ILogger<EventParticipantsController> logger)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _emailSender = emailSender;
            _logger = logger;
        }

        [Authorize]
        public IActionResult Index(string eventId)
        {
            var eventObj = _unitOfWork.Event.Get(x => x.Id == eventId);

            if (eventObj is null)
            {
                return NotFound();
            }

            var eventDetailsVM = new EventDetailsVM
            {
                Event = eventObj
            };

            if (User.IsInRole(SD.Role_Admin))
            {
                eventDetailsVM.Participants = [.. _unitOfWork.EventParticipant.GetAllFiltered(x => x.EventId == eventId, "User")];
            }

            return View(eventDetailsVM);
        }


        public IActionResult _EventParticipantsTable(string eventId)
        {
            // TODO: In case you came up with better way to refresh 
            var Participants = _unitOfWork.EventParticipant.GetAllFiltered(x => x.EventId == eventId, "User").ToList();
            return PartialView("_EventParticipantsTable", Participants);
        }

        [HttpPut]
        public async Task<IActionResult> ChangeParticipationStatus(int participationId, int status)
        {
            var assignmentStatus = (AssignmentStatus)status;
            if (!(new[] { AssignmentStatus.Accepted, AssignmentStatus.Reserve, AssignmentStatus.Declined }).Contains(assignmentStatus))
            {
                return NotFound();
            }

            var participation = _unitOfWork.EventParticipant.Get(x => x.Id == participationId, includes: "Event");
            if (participation is null || participation.Event is null)
            {
                return NotFound();
            }
            // TODO: Sprawdzać czy można jeszcze zapisać jeśli nie to zwracać taką informację
            participation.Status = assignmentStatus;
            _unitOfWork.EventParticipant.Update(participation);
            _unitOfWork.Save();

            if (assignmentStatus == AssignmentStatus.Accepted && !string.IsNullOrEmpty(participation.UserId))
            {
                var user = await _userManager.FindByIdAsync(participation.UserId);

                if (user == null || string.IsNullOrEmpty(user.Email))
                {
                    return Content(TempDataInfos.SendingInformationUserOrEventNotFound);
                }

                var callbackUrl = Request.Scheme + "://" + Request.Host + Url.Action(nameof(Index), new { eventId = participation.EventId });

                if (callbackUrl == null)
                {
                    return Content(TempDataInfos.SendingInformationInvalidLink);
                }

                await _emailSender.SendEmailAsync(
                    user.Email,
                    "Event status information",
                    $"Congratulations, you have been accepted to the {participation.Event.Title}. In case you change your mind and want to refuse here is redirection to our website. <a href='{callbackUrl}'>Click here to go to the event</a>."
                );
            }

            return RedirectToAction(nameof(_EventParticipantsTable), new { participation.EventId });
        }
    }
}
