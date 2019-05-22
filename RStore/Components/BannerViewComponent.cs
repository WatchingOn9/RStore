using Microsoft.AspNetCore.Mvc;
using RStore.Models;

namespace RStore.Components {
    public class BannerViewComponent : ViewComponent {
        private ISettingRepository repository;

        public BannerViewComponent(ISettingRepository repo) {
            repository = repo;
        }

        public IViewComponentResult Invoke() {
            return View(repository.Setting);
        }
    }
}
