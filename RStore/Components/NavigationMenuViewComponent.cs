using Microsoft.AspNetCore.Mvc;
using System.Linq;
using RStore.Models;

namespace RStore.Components {
    public class NavigationMenuViewComponent : ViewComponent {
        private ICategoryRepository repository;

        public NavigationMenuViewComponent(ICategoryRepository repo) {
            repository = repo;
        }

        public IViewComponentResult Invoke(string category) {
            ViewBag.SelectedCategory = category;
            return View(repository.Categories
                .Where(o => !o.Disable && o.ParentID == null)
                .OrderBy(x => x.Order)
                .Distinct());
        }
    }
}
