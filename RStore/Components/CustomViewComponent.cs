using Microsoft.AspNetCore.Mvc;
using RStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RStore.Components {
    public class CustomViewComponent : ViewComponent {
        private IComponentRepository repository;

        public CustomViewComponent(IComponentRepository repo) {
            repository = repo;
        }

        public IViewComponentResult Invoke(string type) {
            return View(repository.Components.Where(x => x.Type == type && !x.Disable).OrderBy(x => x.Order));
        }
    }
}
