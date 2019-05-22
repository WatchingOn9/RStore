using System.Collections.Generic;

namespace RStore.Models.ViewModels {
    public class ComponentEditViewModel {
        public Component Component { get; set; }
        public IEnumerable<string> Types { get; set; }

        public ComponentEditViewModel() {
            Types = new List<string> {
                "SideBar", "Bottom", "Top"
            };
        }
    }
}
