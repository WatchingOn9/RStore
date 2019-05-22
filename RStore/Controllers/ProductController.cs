using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RStore.Models;
using RStore.Models.ViewModels;

namespace RStore.Controllers {
    public class ProductController : Controller {
        private IProductRepository repo;
        private List<Product> products;
        private int currentCatID = 0;

        public ProductController(IProductRepository repository) {
            repo = repository;
            products = new List<Product>();
        }

        public IActionResult Index(int? Id) {
            if (Id != null) {
                var model = new ProductDetailViewModel {
                    Product = repo.Products.FirstOrDefault(x => x.ProductID == Id && !x.Disable)
                };
                if (model.Product != null) {
                    if (model.Product.Category != null) {
                        if (!products.Any() || currentCatID != model.Product.Category.CategoryID) {
                            currentCatID = model.Product.Category.CategoryID;
                            products = repo.Products.Where(x => x.Category.CategoryID == model.Product.Category.CategoryID).OrderBy(x => x.Name).ToList();
                        }
                    }

                    model.CurrentIndex = products.FindIndex(x => x.ProductID == model.Product.ProductID);
                    model.TotalProduct = products.Count;
                    if (model.CurrentIndex - 1 >= 0) {
                        model.PrevProductID = products[model.CurrentIndex - 1].ProductID;
                    }
                    if (model.CurrentIndex + 1 < model.TotalProduct) {
                        model.NextProductID = products[model.CurrentIndex + 1].ProductID;
                    }
                    
                    return View(model);
                }
            } 
            return RedirectToAction("Error", "Error");
        }
    }
}