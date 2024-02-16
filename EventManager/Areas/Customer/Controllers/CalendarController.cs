using EventManager.DataAccess.Repository.IRepository;
using EventManager.Utils;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EventManager.Areas.Customer.Controllers;

[Area(SD.Role_Customer)]
public class CalendarController : Controller
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly UserManager<IdentityUser> _userManager; 
    
    public CalendarController(IUnitOfWork unitOfWork, UserManager<IdentityUser> userManager)
    {
        _unitOfWork = unitOfWork;
        _userManager = userManager;
    }
    
    public IActionResult Index()
    {
        return View();
    }

    [HttpGet]
    public JsonResult GetEvents()
    {
        var userId = _userManager.GetUserId(User);
        var eventParticipations = _unitOfWork.EventParticipant.GetAllFiltered(x => x.UserId == userId, includes: "Event"); 
        var events = eventParticipations.Select(x => x.Event).ToArray();
        
        return Json(events);
    }
}