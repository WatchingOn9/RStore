using System.Linq;

namespace RStore.Models {
    public interface IPageRepository {
        IQueryable<Page> Pages { get; }
        void SavePage(Page page);
        Page DeletePage(int pageID);
    }
}

