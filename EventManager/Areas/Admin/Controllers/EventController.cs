using EventManager.DataAccess.Repository.IRepository;
using EventManager.Models;
using Microsoft.AspNetCore.Mvc;

namespace EventManager.Areas.Admin.Controllers
{
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

        public IActionResult Edit(Guid? id)
        {
            if (!id.HasValue || id.Value == Guid.Empty)
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

        public IActionResult Delete(Guid? id)
        {
            if (!id.HasValue || id.Value == Guid.Empty)
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
        public IActionResult DeletePOST(Guid? id)
        {
            if (!id.HasValue || id.Value == Guid.Empty)
            {
                return View();
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
