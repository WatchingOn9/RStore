using System.Collections.Generic;

namespace RStore.Models.ViewModels {
    public class OrderEditViewModel {
        public Order Order { get; set; }
        public List<CartLine> Lines { get; set; }
    }
}
