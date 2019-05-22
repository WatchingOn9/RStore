using Microsoft.AspNetCore.Mvc;
using RStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RStore.Components {
    public class SlideShowViewComponent : ViewComponent {
        private ISlideShowRepository repository;

        public SlideShowViewComponent(ISlideShowRepository repo) {
            repository = repo;
        }

        public IViewComponentResult Invoke() {
            return View(repository.SlideShows
                .Where(x => !x.Disable)
                .OrderBy(x => x.Order));
        }
    }
}
