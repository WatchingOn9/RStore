using System;
using System.ComponentModel.DataAnnotations;

namespace RStore.Models {
    public class Component {
        public int ComponentID { get; set; }
        [Display(Name = "Component Type")]
        public string Type { get; set; }
        [Display(Name = "Component Name (just a name, will not show)")]
        public string Name { get; set; }
        [Display(Name = "Content")]
        public string Content { get; set; }
        public int Order { get; set; }
        [Display(Name = "Disable?")]
        public bool Disable { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }

        public Component() {
            this.CreatedDate = DateTime.UtcNow;
            this.ModifiedDate = DateTime.UtcNow;
        }
    }
}
