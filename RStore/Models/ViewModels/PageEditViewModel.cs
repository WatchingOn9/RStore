using System.Collections.Generic;

namespace RStore.Models.ViewModels {
    public class PageEditViewModel {
        public Page Page { get; set; }
        public IEnumerable<Page> Pages { get; set; }
        public int? SelectedPage { get; set; }
    }
}
