using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RStore.Models {
    public class Product {

        public int ProductID { get; set; }

        [Display(Name = "Product Code")]
        [Required(ErrorMessage = "Please enter a product code")]
        public string ItemCode { get; set; }

        public string Barcode { get; set; }

        [Required(ErrorMessage = "Please enter a product name")]
        public string Name { get; set; }

        [Display(Name = "Description (Will show in Product Detail Page, support HTML)")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Please enter price currency")]
        public string Currency { get; set; }

        [Required]
        [Range(0.00, double.MaxValue, ErrorMessage = "Please enter a positive price")]
        public decimal Price { get; set; }

        public string UOM { get; set; }

        public int? CategoryID { get; set; }
        public Category Category { get; set; }

        public bool Disable { get; set; }


        [Display(Name = "Thumbnail")]
        public string ThumbImage { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }

        public Product() {
            this.CreatedDate = DateTime.UtcNow;
            this.ModifiedDate = DateTime.UtcNow;
        }

        // Photo Gallery
        private List<Image> images = new List<Image>();
        public virtual IEnumerable<Image> Images => images;
        public virtual void ClearImage() => images.Clear();
        public virtual void AddImage(string imageName) => images.Add(new Image { ImageName = imageName });
        public virtual void RemoveImage(Image image) => images.RemoveAll(l => l.ImageID == image.ImageID);
    }
}
