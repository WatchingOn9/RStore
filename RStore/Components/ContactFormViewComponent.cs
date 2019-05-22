using Microsoft.AspNetCore.Mvc;
using RStore.Models;
using RStore.Models.ViewModels;
using System.Linq;

namespace RStore.Components {
    public class ContactFormViewComponent : ViewComponent {
        private IProductRepository repo;
        public ContactFormViewComponent(IProductRepository repository) {
            repo = repository;
        }
        public IViewComponentResult Invoke(int? productId) {
            if (productId != null) {
                return View(new ContactFormViewModel { Product = repo.Products.FirstOrDefault(x => x.ProductID == productId) });
            } else {
                return View(new ContactFormViewModel());
            }
            
        }
    }
}
