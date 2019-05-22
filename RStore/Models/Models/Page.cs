using System;
using System.ComponentModel.DataAnnotations;

namespace RStore.Models {
    public class Page {
        public int PageID { get; set; }
        [Display(Name = "Page Name (just a name, will not show)")]
        public string PageName { get; set; }
        [Display(Name = "Title (will be shown on top navigation bar, support HTML)")]
        public string Title { get; set; }
        [Display(Name = "Page Content")]
        public string Content { get; set; }
        public int? ParentID { get; set; }
        public Page ParentPage { get; set; }
        public int Order { get; set; }
        [Display(Name = "Disable?")]
        public bool Disable { get; set; }
        [Display(Name = "Is Show Product?")]
        public bool ShowProduct { get; set; }
        [Display(Name = "Is Show Enquiry Form?")]
        public bool ShowEnquiryForm { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }

        public Page() {
            this.CreatedDate = DateTime.UtcNow;
            this.ModifiedDate = DateTime.UtcNow;
        }
    }
}
