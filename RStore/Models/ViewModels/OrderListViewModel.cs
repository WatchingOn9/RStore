using System.Collections.Generic;

namespace RStore.Models.ViewModels {
    public class OrderListViewModel {
        public IEnumerable<Order> Orders { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
}
