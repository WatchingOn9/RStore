using Microsoft.AspNetCore.Mvc;
using RStore.Models;
using RStore.Models.ViewModels;
using System.Linq;
using Microsoft.AspNetCore.Authorization;

namespace RStore.Controllers {
    [Authorize]
    public class AdminCategoryController : Controller {
        private ICategoryRepository repository;

        public AdminCategoryController(ICategoryRepository repo) {
            repository = repo;
        }

        public ViewResult Index() {
            var model = repository.Categories;
            return View(model);
        }

        public ViewResult Edit(int categoryId) {
            var model = new CategoryViewModel {
                Category = repository.Categories.FirstOrDefault(c => c.CategoryID == categoryId),
                Categories = repository.Categories.Where(c => c.CategoryID != categoryId)
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(CategoryViewModel model) {
            if (model.Category.ParentID != null) {
                model.Category.ParentCategory = repository.Categories.FirstOrDefault(p => p.CategoryID == model.Category.ParentID);
            }

            if (ModelState.IsValid) {
                repository.SaveCategory(model.Category);
                TempData["message"] = $"{model.Category.CategoryName} has been saved";
                return RedirectToAction("Index");
            } else {
                TempData["error"] = "The are something error in following fields. Please check.";
                model.Categories = repository.Categories.Where(c => c.CategoryID != model.Category.CategoryID);
                return View(model);
            }
        }

        public ViewResult Create() {
            var model = new CategoryViewModel {
                Category = new Category(), 
                Categories = repository.Categories
            };
            return View("Edit", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int categoryId) {
            Category deletedCategory = repository.DeleteCategory(categoryId);
            if (deletedCategory != null) {
                TempData["message"] = $"{deletedCategory.CategoryName} was deleted";
            }
            return RedirectToAction("Index");
        }
    }

    
}