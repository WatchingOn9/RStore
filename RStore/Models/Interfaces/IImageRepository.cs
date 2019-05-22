using System.Linq;

namespace RStore.Models {
    public interface IImageRepository {
        IQueryable<Image> Images { get; }
        Image DeleteImage(int imageID);
    }
}
