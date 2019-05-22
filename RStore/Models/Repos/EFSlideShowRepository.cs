using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace RStore.Models {
    public class EFSlideShowRepository : ISlideShowRepository {
        private AppDbContext context;

        public EFSlideShowRepository(AppDbContext ctx) {
            context = ctx;
        }

        public IQueryable<SlideShow> SlideShows => context.SlideShows;

        public void SaveSlideShow(SlideShow slideshow) {
            if (slideshow.SlideShowID == 0) {
                context.SlideShows.Add(slideshow);
            } else {
                SlideShow dbEntry = context.SlideShows
                    .FirstOrDefault(p => p.SlideShowID == slideshow.SlideShowID);
                if (dbEntry != null) {
                    dbEntry.Order = slideshow.Order;
                    dbEntry.Description = slideshow.Description;
                    dbEntry.Disable = slideshow.Disable;
                    dbEntry.Image = slideshow.Image;
                    dbEntry.Name = slideshow.Name;
                    dbEntry.ModifiedDate = DateTime.UtcNow;
                }
            }
            context.SaveChanges();
        }

        public SlideShow DeleteSlideShow(int slideshowID) {
            SlideShow dbEntry = context.SlideShows
                .FirstOrDefault(p => p.SlideShowID == slideshowID);
            if (dbEntry != null) {
                context.SlideShows.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }
    }
}
