using System.Net;
using EventManager.DataAccess;
using EventManager.DataAccess.Repository.IRepository;
using EventManager.Models;
using EventManager.Models.ViewModels;
using EventManager.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static EventManager.Utils.TempDataInfos;
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

            var eventDetailsVm = new EventDetailsVM
            {
                Event = eventObj,
                IsFull = _unitOfWork.EventParticipant.GetAllFilteredCount(x => x.EventId == eventObj.Id && x.Status == AssignmentStatus.Accepted) >= eventObj.Capacity
            };

            eventDetailsVm.Participants = [.. _unitOfWork.EventParticipant.GetAllFiltered(x => x.EventId == eventId, "User")];

            return View(eventDetailsVm);
        }


        public IActionResult _EventParticipantsTable(string eventId)
        {
            // TODO: In case you came up with better way to refresh 
            var participants = _unitOfWork.EventParticipant.GetAllFiltered(x => x.EventId == eventId, "User").ToList();
            return PartialView("_EventParticipantsTable", participants);
        }

        [HttpPut]
        public async Task<IActionResult> ChangeParticipationStatus(int participationId, int status)
        {
            var assignmentStatus = (AssignmentStatus)status;
            if (!(new[] { AssignmentStatus.Accepted, AssignmentStatus.Reserve, AssignmentStatus.Declined }).Contains(assignmentStatus))
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new { success = false, message = WrongStatus });
            }

            var participation = _unitOfWork.EventParticipant.Get(x => x.Id == participationId, includes: "Event");
            if (participation?.Event is null)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new { success = false, message = EventNotFound });
            }

            if (assignmentStatus == AssignmentStatus.Accepted && IsEventFull(participation.Event))
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new { success = false, message = EventIsFull });
            }
            
            participation.Status = assignmentStatus;
            _unitOfWork.EventParticipant.Update(participation);
            _unitOfWork.Save();

            if (assignmentStatus != AssignmentStatus.Accepted || string.IsNullOrEmpty(participation.UserId))
            {
                return RedirectToAction(nameof(_EventParticipantsTable), new { participation.EventId });
            }
            
            var user = await _userManager.FindByIdAsync(participation.UserId);

            if (user == null || string.IsNullOrEmpty(user.Email))
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new { success = false, message = EmailNotFound });
            }

            var callbackUrl = Request.Scheme + "://" + Request.Host + Url.Action(nameof(Index), new { eventId = participation.EventId });

            await _emailSender.SendEmailAsync(
                user.Email,
                "Event status information",
                $"Congratulations, you have been accepted to the {participation.Event.Title}. In case you change your mind and want to refuse here is redirection to our website. <a href='{callbackUrl}'>Click here to go to the event</a>."
            );

            return RedirectToAction(nameof(_EventParticipantsTable), new { participation.EventId });
        }
        
        public bool IsEventFull(Event obj)
        {
            return _unitOfWork.EventParticipant.GetAllFiltered(x => x.EventId == obj.Id && x.Status == AssignmentStatus.Accepted).Count() >= obj.Capacity;
        }
    }
}
