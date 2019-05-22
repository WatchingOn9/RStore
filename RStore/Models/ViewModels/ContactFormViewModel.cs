using System.ComponentModel.DataAnnotations;

namespace RStore.Models.ViewModels {
    public class ContactFormViewModel {
        [Display(Name = "Your Name: ")]
        public string Name { get; set; }
        [Display(Name = "Company Name: ")]
        public string Company { get; set; }
        public string Email { get; set; }
        [Display(Name = "Contact No.: ")]
        public string ContactNo { get; set; }
        [Display(Name = "Product Interested: ")]
        public string ProductInterested { get; set; }
        [Display(Name = "Quantity: ")]
        public int Qty { get; set; }
        [Display(Name = "Ship To Name: ")]
        public string ShipToName { get; set; }
        [Display(Name = "Ship To Address: ")]
        public string ShipToAddress { get; set; }
        [Display(Name = "Message: ")]
        public string Message { get; set; }

        public Product Product { get; set; }
    }
}
