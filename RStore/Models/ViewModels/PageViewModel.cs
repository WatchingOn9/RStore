using System.Collections.Generic;
using RStore.Models;

namespace RStore.Models.ViewModels {
    public class PageViewModel {
        public Page Page { get; set; }
        public string CurrentCategory { get; set; }
        public int ProductPage { get; set; }
    }
}
