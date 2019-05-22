using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RStore.Models;
using RStore.Models.ViewModels;

namespace RStore.Controllers {
    public class PageController : Controller {
        private IPageRepository repository;

        public PageController(IPageRepository repo) {
            repository = repo;
        }

        public IActionResult Index(int? Id, string category, int page = 1) {
            ViewBag.SelectedCategory = category;
            if (Id != null && Id != 0) {
                if (repository.Pages.Any(x => x.PageID == Id && !x.Disable)) {
                    return View(new PageViewModel {
                        Page = repository.Pages.FirstOrDefault(x => x.PageID == Id && !x.Disable),
                        CurrentCategory = category,
                        ProductPage = page
                    });
                }
            }
            return RedirectToAction("Error", "Error");
        }

        public IActionResult Google() {
            return File("~/google0549c693d710c2b3.html", "text/html");
        }

        public IActionResult Sitemap() {
            return File("~/sitemap.xml", "text/xml");
        }

        public IActionResult RobotTxt() {
            return File("~/robots.txt", "text/plain");
        }


    }
}