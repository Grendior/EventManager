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

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Event eventObj)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            TempData["success"] = "Testowanie notyfikacji";
            _unitOfWork.Event.Add(eventObj);
            _unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(string? id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }
            var eventFromDb = _unitOfWork.Event.Get(x => x.Id == id);
            if (eventFromDb == null)
            {
                return NotFound();
            }

            return View(eventFromDb);
        }

        [HttpPost]
        public IActionResult Edit(Event eventObj)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            _unitOfWork.Event.Update(eventObj);
            _unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(string? id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            var eventFromDb = _unitOfWork.Event.Get(x => x.Id == id);
            if (eventFromDb == null)
            {
                return NotFound();
            }

            return View(eventFromDb);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(string? id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            var eventFromDb = _unitOfWork.Event.Get(x => x.Id == id);
            if (eventFromDb == null)
            {
                return NotFound();
            }

            _unitOfWork.Event.Remove(eventFromDb);
            _unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }
    }
}
