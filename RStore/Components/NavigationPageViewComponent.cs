using Microsoft.AspNetCore.Mvc;
using RStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RStore.Components {
    public class NavigationPageViewComponent : ViewComponent {
        private IPageRepository repository;

        public NavigationPageViewComponent(IPageRepository repo) {
            repository = repo;
        }

        public IViewComponentResult Invoke() {
            return View(repository.Pages
                .Where(o => !o.Disable && o.ParentID == null)
                .OrderBy(x => x.Order)
                .Distinct());
        }
    }
}
