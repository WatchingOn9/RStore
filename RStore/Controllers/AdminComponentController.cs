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
    public class AdminComponentController : Controller {
        private IComponentRepository _cRepo;

        public AdminComponentController(IComponentRepository cRepo) {
            _cRepo = cRepo;
        }

        public IActionResult Index() => View(_cRepo.Components.OrderBy(x => x.Order));

        public ViewResult Edit(int componentId) {
            return View(new ComponentEditViewModel {
                Component = _cRepo.Components.FirstOrDefault(s => s.ComponentID == componentId),
            });
        }

        public ViewResult Create() {
            return View("Edit", new ComponentEditViewModel { Component = new Component() });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ComponentEditViewModel model) {
            if (ModelState.IsValid) {
                _cRepo.SaveComponent(model.Component);
                TempData["message"] = $"{model.Component.Name} has been saved";
                return RedirectToAction("Index");
            } else {
                TempData["error"] = "The are something error in following fields. Please check.";
                return View(model);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int componentId) {
            Component deletedComponent = _cRepo.DeleteComponent(componentId);
            if (deletedComponent != null) {
                TempData["message"] = $"{deletedComponent.Name} was deleted";
            }
            return RedirectToAction("Index");
        }
    }
}