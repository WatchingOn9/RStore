using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RStore.Models {
    public class Category {
        public int CategoryID { get; set; }
        [Display(Name = "Category Name")]
        [Required(ErrorMessage = "Please enter a Category Name")]
        public string CategoryName { get; set; }
        [Display(Name = "Description (Will show in product page, support HTML)")]
        public string Description { get; set; }
        public int? ParentID { get; set; }
        [Display(Name = "Parent Category")]
        public Category ParentCategory { get; set; }
        [Display(Name = "Disable? (If disable will not shown)")]
        public bool Disable { get; set; }
        [Display(Name = "Is Main Category? (Will show in footer section)")]
        public bool IsMainCategory { get; set; }
        public int Order { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }

        // Photo Gallery
        private List<Category> childs = new List<Category>();
        public virtual IEnumerable<Category> Childs => childs;

        public Category() {
            this.CreatedDate = DateTime.UtcNow;
            this.ModifiedDate = DateTime.UtcNow;
        }
    }
}
