using Microsoft.AspNetCore.Mvc;
using RStore.Models;
using RStore.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RStore.Components {
    public class FooterViewComponent : ViewComponent {
        private ISettingRepository _sRepo;
        private ICategoryRepository _cRepo;

        public FooterViewComponent(ISettingRepository sRepo, ICategoryRepository cRepo) {
            _sRepo = sRepo;
            _cRepo = cRepo;
        }

        public IViewComponentResult Invoke() {
            var model = new FooterViewModel {
               Setting = _sRepo.Setting,
               Categories = _cRepo.Categories.Where(x => !x.Disable && x.IsMainCategory).OrderBy(x => x.Order)
            };
            return View(model);
        }
    }
}
