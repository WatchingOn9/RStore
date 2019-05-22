using Microsoft.AspNetCore.Mvc;
using RStore.Models;
using RStore.Models.ViewModels;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;

namespace RStore.Controllers {
    [Authorize]
    public class AdminProductController : Controller {
        private IProductRepository _prodRepo;
        private ICategoryRepository _catRepo;

        public AdminProductController(IProductRepository prodRepo, ICategoryRepository catRepo) {
            _prodRepo = prodRepo;
            _catRepo = catRepo;
        }

        public ViewResult Index() => View(_prodRepo.Products);

        public ViewResult Edit(int productId) {
            var model = new ProductEditViewModel {
                Product = _prodRepo.Products.FirstOrDefault(p => p.ProductID == productId),
                Categories = _catRepo.Categories
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ProductEditViewModel model, IFormFile thumbimage, List<IFormFile> files) {
            if (model.Product.CategoryID != null) {
                model.Product.Category = _catRepo.Categories.FirstOrDefault(p => p.CategoryID == model.Product.CategoryID);
            } 

            if (ModelState.IsValid) {
                files.Insert(0, thumbimage);

                if (files != null) {
                    for (int i = 0; i < files.Count; i++) {
                        if (files[i] != null && files[i].Length > 0) {
                            var fileName = DateTime.Now.ToString("yyyyMMddHHmmss") + "_" + Path.GetFileName(files[i].FileName);
                            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images\\products", fileName);

                            if (i == 0) {
                                model.Product.ThumbImage = fileName;
                            } else {
                                model.Product.AddImage(fileName);
                            }

                            using (var fileSteam = new FileStream(filePath, FileMode.Create)) {
                                await files[i].CopyToAsync(fileSteam);
                            }
                        }
                    }
                }

                _prodRepo.SaveProduct(model.Product);
                TempData["message"] = $"{model.Product.Name} has been saved";
                return RedirectToAction("Index");
            } else {
                TempData["error"] = "The are something error in following fields. Please check.";
                model.Categories = _catRepo.Categories;
                return View(model);
            }
        }

        public ViewResult Create() {
            var model = new ProductEditViewModel {
                Product = new Product(),
                Categories = _catRepo.Categories
            };
            return View("Edit", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int productId) {
            Product deletedProduct = _prodRepo.DeleteProduct(productId);
            if (deletedProduct != null) {
                if (!string.IsNullOrEmpty(deletedProduct.ThumbImage)) {
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images\\products", deletedProduct.ThumbImage);
                    System.IO.File.Delete(filePath);
                }

                if (deletedProduct.Images.Any()) {
                    foreach (var image in deletedProduct.Images) {
                        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images\\products", image.ImageName);
                        System.IO.File.Delete(filePath);
                    }
                }

                TempData["message"] = $"{deletedProduct.Name} was deleted";
            }
            return RedirectToAction("Index");
        }

        public IActionResult RemoveAllImages(int productId) {
            var images = _prodRepo.RemoveAllImages(productId);
            if (images != null) {
                if (images.Any()) {
                    foreach (var image in images) {
                        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images\\products", image);
                        System.IO.File.Delete(filePath);
                    }
                }

                TempData["message"] = $"Images was removed";
            }
            return RedirectToAction("Edit", new { productId = productId });
        }
    }
}