using System.Collections.Generic;

namespace RStore.Models.ViewModels {
    public class CategoryViewModel {
        public Category Category { get; set; }
        public IEnumerable<Category> Categories { get; set; }
    }
}
