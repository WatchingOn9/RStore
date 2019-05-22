using Microsoft.AspNetCore.Mvc;
using RStore.Models;
using System.Linq;
using RStore.Models.ViewModels;

namespace RStore.Components {
    public class ProductListViewComponent : ViewComponent {
        private IProductRepository repository;
        private ICategoryRepository catRepo;
        public int PageSize = 100;

        public ProductListViewComponent(IProductRepository repo, ICategoryRepository cat) {
            repository = repo;
            catRepo = cat;
        }

        public IViewComponentResult Invoke(string category, int page = 1) {
            string catDesc = "";
            if (!string.IsNullOrEmpty(category)) {
                if (catRepo.Categories.Any(x => x.CategoryName == category)) {
                    catDesc = catRepo.Categories.FirstOrDefault(x => x.CategoryName == category).Description;
                }
            }
            return View(new ProductsListViewModel {
                Products = repository.Products
                    .Where(p => !p.Disable && (category == null || p.Category.CategoryName == category))
                    .OrderByDescending(p => p.CreatedDate)
                    .Skip((page - 1) * PageSize)
                    .Take(PageSize),
                PagingInfo = new PagingInfo {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = category == null ?
                        repository.Products.Where(x => !x.Disable).Count() :
                        repository.Products.Where(e =>
                            e.Category.CategoryName == category && !e.Disable).Count()
                },
                CurrentCategory = category,
                CurrentCategoryDesc = catDesc
            });
        }

    }
}