using RStore.Models;
using System.Collections.Generic;

namespace RStore.Models.ViewModels {
    public class CartIndexViewModel {
        public Cart Cart { get; set; }
        public Order Order { get; set; }
        public string ReturnUrl { get; set; }
    }
}
