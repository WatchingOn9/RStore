using System.Linq;

namespace RStore.Models {
    public interface IOrderRepository {
        IQueryable<Order> Orders { get; }
        void SaveOrder(Order order);
    }
}
