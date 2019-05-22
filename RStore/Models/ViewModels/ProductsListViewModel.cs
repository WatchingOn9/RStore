using System.Collections.Generic;
using RStore.Models;

namespace RStore.Models.ViewModels {
    public class ProductsListViewModel {
        public IEnumerable<Product> Products { get; set; }
        public PagingInfo PagingInfo { get; set; }
        public string CurrentCategory { get; set; }
        public string CurrentCategoryDesc { get; set; }
    }
}
