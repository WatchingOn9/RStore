using System.Linq;

namespace RStore.Models {
    public interface ISlideShowRepository {
        IQueryable<SlideShow> SlideShows { get; }
        void SaveSlideShow(SlideShow slideshow);
        SlideShow DeleteSlideShow(int SlideShow);
    }
}
