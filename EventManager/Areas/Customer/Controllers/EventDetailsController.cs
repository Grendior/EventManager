using EventManager.DataAccess;
using EventManager.DataAccess.Repository.IRepository;
using EventManager.Models.ViewModels;
using EventManager.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static EventManager.Utils.Enums;

namespace EventManager.Areas.Customer.Controllers
{
    [Area(SD.Role_Customer)]
    public class EventDetailsController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<EventParticipantsController> _logger;

        public EventDetailsController(ApplicationDbContext dbContext, IUnitOfWork unitOfWork, ILogger<EventParticipantsController> logger)
        {
            _dbContext = dbContext;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

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

        [HttpPut]
        public IActionResult ChangeParticipationStatus(int participationId, int status)
        {
            var assignmentStatus = (AssignmentStatus)status;
            if (!(new[] { AssignmentStatus.Accepted, AssignmentStatus.Reserve, AssignmentStatus.Declined }).Contains(assignmentStatus))
            {
                return NotFound();
            }

            var participation = _unitOfWork.EventParticipant.Get(x => x.Id == participationId);
            if (participation is null)
            {
                return NotFound();
            }

            participation.Status = assignmentStatus;
            _unitOfWork.EventParticipant.Update(participation);
            _unitOfWork.Save();

            return RedirectToAction(nameof(Index), new { participation.EventId });
        }
    }
}
