using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RStore.Models.ViewModels {
    public class FooterViewModel {
        public Setting Setting { get; set; }
        public IEnumerable<Category> Categories { get; set; }
    }
}
