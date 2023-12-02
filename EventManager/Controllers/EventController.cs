using EventManager.DataAccess;
using EventManager.Models;
using Microsoft.AspNetCore.Mvc;

namespace EventManager.Controllers
{
    public class EventController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        public EventController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            var objEventList = _dbContext.Events.ToList();
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
            _dbContext.Events.Add(eventObj);
            _dbContext.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(Guid? id)
        {
            if (!id.HasValue || id.Value == Guid.Empty)
            {
                return NotFound();
            }
            var eventFromDb = _dbContext.Events.FirstOrDefault(x => x.Id == id);
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

            _dbContext.Events.Update(eventObj);
            _dbContext.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(Guid? id)
        {
            if (!id.HasValue || id.Value == Guid.Empty)
            {
                return NotFound();
            }

            var eventFromDb = _dbContext.Events.FirstOrDefault(x => x.Id == id);
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

            var eventFromDb = _dbContext.Events.FirstOrDefault(x => x.Id == id);
            if (eventFromDb == null)
            {
                return NotFound();
            }

            _dbContext.Events.Remove(eventFromDb);
            _dbContext.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
