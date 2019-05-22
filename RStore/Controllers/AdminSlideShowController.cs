using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RStore.Models;

namespace RStore.Controllers {
    [Authorize]
    public class AdminSlideShowController : Controller {
        private ISlideShowRepository _sRepo;

        public AdminSlideShowController(ISlideShowRepository sRepo) {
            _sRepo = sRepo;
        }

        public IActionResult Index() => View(_sRepo.SlideShows.OrderBy(x => x.Order));

        public ViewResult Edit(int slideShowId) => View(_sRepo.SlideShows.FirstOrDefault(s => s.SlideShowID == slideShowId));

        public ViewResult Create() => View("Edit", new SlideShow());

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(SlideShow model, IFormFile file) {
            if (ModelState.IsValid) {
                if (file != null) {
                    var fileName = DateTime.Now.ToString("yyyyMMddHHmmss") + "_" + Path.GetFileName(file.FileName);
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images\\slideshows", fileName);
                    model.Image = fileName;
                    using (var fileSteam = new FileStream(filePath, FileMode.Create)) {
                        await file.CopyToAsync(fileSteam);
                    }
                }

                _sRepo.SaveSlideShow(model);
                TempData["message"] = $"{model.Name} has been saved";
                return RedirectToAction("Index");
            } else {
                TempData["error"] = "The are something error in following fields. Please check.";
                return View(model);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int slideShowId) {
            SlideShow deletedSlideShow = _sRepo.DeleteSlideShow(slideShowId);
            if (deletedSlideShow != null) {
                if (!string.IsNullOrEmpty(deletedSlideShow.Image)) {
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images\\slideshows", deletedSlideShow.Image);
                    System.IO.File.Delete(filePath);
                }
                TempData["message"] = $"{deletedSlideShow.Name} was deleted";
            }
            return RedirectToAction("Index");
        }
    }
}