using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RStore.Infrastructure;
using RStore.Models;

namespace RStore.Controllers {
    [Authorize]
    public class AdminSettingController : Controller {
        private ISettingRepository _sRepo;

        public AdminSettingController(ISettingRepository sRepo) {
            _sRepo = sRepo;
        }

        public IActionResult Index() => View(_sRepo.Setting == null ? new Setting() : _sRepo.Setting);

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(Setting setting, IFormFile logo, IFormFile adminLogo) {
            if (ModelState.IsValid) {
                if (logo != null) {
                    var fileName = Path.GetFileName(logo.FileName);
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images", fileName);
                    setting.Logo = fileName;
                    using (var fileSteam = new FileStream(filePath, FileMode.Create)) {
                        await logo.CopyToAsync(fileSteam);
                    }
                }

                if (adminLogo != null) {
                    var fileName = Path.GetFileName(adminLogo.FileName);
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images", fileName);
                    setting.AdminLogo = fileName;
                    using (var fileSteam = new FileStream(filePath, FileMode.Create)) {
                        await adminLogo.CopyToAsync(fileSteam);
                    }
                }
                _sRepo.SaveSetting(setting);
                TempData["message"] = "Setting has been saved";
            } else {
                TempData["error"] = "The are something error in following fields. Please check.";
            }
            return View(setting);
        }
    }
}