using EventManager.DataAccess.Repository.IRepository;
using EventManager.Utils;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EventManager.Areas.Admin.Controllers;

[Area(SD.Role_Admin)]
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
        var events = _unitOfWork.Event.GetAllFiltered(x => x.CreatorId == userId).ToArray();
        
        return Json(events);
    }
}