using Microsoft.AspNetCore.Mvc;
using RStore.Models;

namespace RStore.Components {
    public class SiteTitleViewComponent : ViewComponent {
        private ISettingRepository repository;

        public SiteTitleViewComponent(ISettingRepository repo) {
            repository = repo;
        }

        public IViewComponentResult Invoke() {
            return View(repository.Setting);
        }
    }
}
