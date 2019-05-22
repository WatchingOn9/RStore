using System.Linq;

namespace RStore.Models {
    public interface ICategoryRepository {
        IQueryable<Category> Categories { get; }
        void SaveCategory(Category category);
        Category DeleteCategory(int categoryID);
    }
}
