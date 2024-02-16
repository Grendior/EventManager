using System.Net;
using EventManager.DataAccess.Repository.IRepository;
using EventManager.Models;
using EventManager.Services;
using EventManager.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using static EventManager.Utils.TempDataInfos;

namespace EventManager.Areas.Admin.Controllers
{
    [Area(SD.Role_Admin)]
    [Authorize(Roles = SD.Role_Admin)]
    public class EventController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly FileService _fileService;

        public EventController(IUnitOfWork unitOfWork, FileService fileService, UserManager<IdentityUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _fileService = fileService;
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
                var eventObj = new Event
                {
                    CreatorId = _userManager.GetUserId(User)
                };
                return View(eventObj);
            }

            var eventFromDb = _unitOfWork.Event.Get(x => x.Id == id);
            if (eventFromDb is not null)
            {
                return View(eventFromDb);
            }

            TempData["error"] = EventNotFound;
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Upsert(Event eventObj, IFormFile? file)
        {
            if (!ModelState.IsValid)
            {
                TempData["error"] = FillOutForm;
                return View();
            }
            
            try
            {
                if (file != null)
                {
                    if (!string.IsNullOrEmpty(eventObj.ImageUrl))
                    {
                        var oldImageName = eventObj.ImageUrl.Split('/').Last();

                        await _fileService.DeleteAsync(oldImageName);
                    }

                    var responseFromBlob = await _fileService.UploadAsync(file);

                    eventObj.ImageUrl = responseFromBlob.Blob.Uri;
                }

                if (string.IsNullOrEmpty(eventObj.Id))
                {
                    eventObj.Id = Guid.NewGuid().ToString();
                    _unitOfWork.Event.Add(eventObj);
                    TempData["success"] = EventSuccessfullyCreated;
                }
                else
                {
                    _unitOfWork.Event.Update(eventObj);
                    TempData["success"] = EventSuccessfullyUpdated;
                }

                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                TempData["error"] = EventSomethingWrong;
                return View();
            }
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(string? id)
        {
            if (string.IsNullOrEmpty(id))
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new { success = false, message = EventNotFound });
            }

            var eventFromDb = _unitOfWork.Event.Get(x => x.Id == id);
            if (eventFromDb is null)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new { success = false, message = EventNotFound });
            }

            try
            {
                // Delete any participants of this event
                var listOfEventParticipants =
                    _unitOfWork.EventParticipant.GetAllFiltered(x => x.EventId == eventFromDb.Id).ToList();
                if (listOfEventParticipants.Count != 0)
                {
                    _unitOfWork.EventParticipant.RemoveRange(listOfEventParticipants);
                    _unitOfWork.Save();
                }

                // Delete image if exists
                if (!string.IsNullOrEmpty(eventFromDb.ImageUrl))
                {
                    var oldImageName = eventFromDb.ImageUrl.Split('/').Last();

                    await _fileService.DeleteAsync(oldImageName);
                }

                _unitOfWork.Event.Remove(eventFromDb);
                _unitOfWork.Save();
                TempData["success"] = SuccessfulDeletionOfEvent;
                return Json(new { success = true, message = SuccessfulDeletionOfEvent });
            }
            catch (Exception)
            {
                TempData["error"] = ErrorDuringEventDelete;
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new { success = false, message = SuccessfulDeletionOfEvent });
            }
        }
    }
}