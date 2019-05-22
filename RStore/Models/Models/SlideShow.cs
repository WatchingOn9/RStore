using System;
using System.ComponentModel.DataAnnotations;

namespace RStore.Models {
    public class SlideShow {
        public int SlideShowID { get; set; }
        [Display(Name = "Name (Optional, will show at the picture bottom)")]
        public string Name { get; set; }
        [Display(Name = "Description (Optional, will show at the picture bottom)")]
        public string Description { get; set; }
        public string Image { get; set; }
        public bool Disable { get; set; }
        public int Order { set; get; }

        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }

        public SlideShow() {
            this.CreatedDate = DateTime.UtcNow;
            this.ModifiedDate = DateTime.UtcNow;
        }
    }
}
