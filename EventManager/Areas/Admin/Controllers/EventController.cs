using EventManager.DataAccess.Repository.IRepository;
using EventManager.Models;
using EventManager.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EventManager.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class EventController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public EventController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            var objEventList = _unitOfWork.Event.GetAll().ToList();
            return View(objEventList);
        }

        public IActionResult Upsert(string? id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return View(new Event());
            }

            var eventFromDb = _unitOfWork.Event.Get(x => x.Id == id);
            if (eventFromDb is null)
            {
                return NotFound();
            }

            return View(eventFromDb);
        }

        [HttpPost]
        public IActionResult Upsert(Event eventObj)
        {
            if (!ModelState.IsValid)
            {
                TempData["error"] = "Please fill out the form correctly";
                return View();
            }

            if (string.IsNullOrEmpty(eventObj.Id))
            {
                eventObj.Id = Guid.NewGuid().ToString();
                _unitOfWork.Event.Add(eventObj);
                TempData["success"] = "Event was successfully created";
            }
            else
            {
                _unitOfWork.Event.Update(eventObj);
                TempData["success"] = "Event was successfully updated";
            }

            _unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }

        [HttpDelete]
        public IActionResult Delete(string? id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return Json(new { success = false, message = "There is no userId passed" });
            }

            var eventFromDb = _unitOfWork.Event.Get(x => x.Id == id);
            if (eventFromDb is null)
            {
                return Json(new { success = false, message = "There is no such event" });
            }

            _unitOfWork.Event.Remove(eventFromDb);
            _unitOfWork.Save();
            TempData["success"] = "Event has been successfully deleted";
            return Json(new { success = true, message = "Event has been successfully deleted" });
        }
    }
}
