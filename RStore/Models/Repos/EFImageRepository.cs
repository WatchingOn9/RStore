using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace RStore.Models {
    public class EFImageRepository : IImageRepository {
        private AppDbContext context;

        public EFImageRepository(AppDbContext ctx) {
            context = ctx;
        }

        public IQueryable<Image> Images => context.Images;

        public Image DeleteImage(int imageID) {
            Image dbEntry = context.Images
                .FirstOrDefault(p => p.ImageID == imageID);
            if (dbEntry != null) {
                context.Images.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }

    }
}
