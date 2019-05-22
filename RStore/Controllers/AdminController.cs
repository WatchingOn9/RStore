using Microsoft.AspNetCore.Mvc;
using RStore.Models;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;
using RStore.Models.ViewModels;

namespace RStore.Controllers {
    [Authorize]
    public class AdminController : Controller {
        private IProductRepository _pRepo;
        private IOrderRepository _oRepo;
        private ICategoryRepository _cRepo;
        private ISlideShowRepository _sRepo;

        public AdminController(IProductRepository pRepo, IOrderRepository oRepo, 
            ICategoryRepository cRepo, ISlideShowRepository sRepo) {
            _pRepo = pRepo;
            _oRepo = oRepo;
            _cRepo = cRepo;
            _sRepo = sRepo;
        }

        public ViewResult Index() {
            var model = new DashboardViewModel {
                ActiveCategoryCount = _cRepo.Categories.Count(x => !x.Disable),
                TotalCategoryCount = _cRepo.Categories.Count(),
                ActiveProductCount = _pRepo.Products.Count(x => !x.Disable),
                TotalProductCount = _pRepo.Products.Count(),
                ActiveOrderCount = _oRepo.Orders.Count(x => !x.Sent && !x.Cancelled),
                TotalOrderCount = _oRepo.Orders.Count(x => !x.Cancelled),
                ActiveSlideShowCount = _sRepo.SlideShows.Count(x => !x.Disable),
                TotalSlideShowCount = _sRepo.SlideShows.Count()
            };
            return View(model);
        }

    }
}