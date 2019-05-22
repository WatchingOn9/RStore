using System.Collections.Generic;

namespace RStore.Models.ViewModels {
    public class ProductEditViewModel {
        public Product Product { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        
    }
}
