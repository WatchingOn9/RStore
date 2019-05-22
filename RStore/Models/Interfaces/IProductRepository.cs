using System.Collections.Generic;
using System.Linq;

namespace RStore.Models {
    public interface IProductRepository {
        IQueryable<Product> Products { get; }
        void SaveProduct(Product product);
        Product DeleteProduct(int productID);
        IEnumerable<string> RemoveAllImages(int productID);
    }
}
