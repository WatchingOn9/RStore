using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RStore.Models;
using RStore.Models.ViewModels;

namespace RStore.Controllers {
    [Authorize]
    public class AdminPageController : Controller {
        private IPageRepository _pRepo;

        public AdminPageController(IPageRepository pRepo) {
            _pRepo = pRepo;
        }

        public IActionResult Index() => View(_pRepo.Pages.OrderBy(x => x.Order));

        public ViewResult Edit(int pageId) {
            var model = new PageEditViewModel {
                Page = _pRepo.Pages.FirstOrDefault(s => s.PageID == pageId),
                Pages = _pRepo.Pages.Where(c => c.PageID != pageId)
            };
            return View(model);
        }

        public ViewResult Create() {
            var model = new PageEditViewModel {
                Page = new Page(),
                Pages = _pRepo.Pages
            };
            return View("Edit", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(PageEditViewModel model) {
            if (model.Page.ParentID != null) {
                model.Page.ParentPage = _pRepo.Pages.FirstOrDefault(p => p.ParentID == model.Page.ParentID);
            } 

            if (ModelState.IsValid) {
                _pRepo.SavePage(model.Page);
                TempData["message"] = $"{model.Page.PageName} has been saved";
                return RedirectToAction("Index");
            } else {
                TempData["error"] = "The are something error in following fields. Please check.";
                model.Pages = _pRepo.Pages.Where(c => c.PageID != model.Page.PageID);
                return View(model);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int pageId) {
            Page deletedPage = _pRepo.DeletePage(pageId);
            if (deletedPage != null) {
                TempData["message"] = $"{deletedPage.PageName} was deleted";
            }
            return RedirectToAction("Index");
        }
    }
}