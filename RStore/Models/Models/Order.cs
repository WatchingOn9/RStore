using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace RStore.Models {
    public class Order {
        public int OrderID { get; set; }
        [BindNever]
        public ICollection<CartLine> Lines { get; set; }
        [BindNever]
        public bool Sent { get; set; }
        [BindNever]
        public bool Cancelled { get; set; }

        [Required(ErrorMessage = "Please Enter Company Name")]
        public string CompanyName { get; set; }

        public string ContactPerson { get; set; }

        [Required(ErrorMessage = "Please Enter Phone No.")]
        public string PhoneNo { get; set; }

        [Required(ErrorMessage = "Please Enter Email Address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please enter the first address line")]
        public string Line1 { get; set; }
        public string Line2 { get; set; }
        public string Line3 { get; set; }

        [Required(ErrorMessage = "Please enter a city name")]
        public string City { get; set; }

        [Required(ErrorMessage = "Please enter a state name")]
        public string State { get; set; }

        public string Zip { get; set; }

        [Required(ErrorMessage = "Please enter a country name")]
        public string Country { get; set; }

        public string CustomerRemark { get; set; }

        public string FreeGift { get; set; }

        [Range(0.00, double.MaxValue, ErrorMessage = "Please enter a positive price")]
        public decimal? Tax { get; set; }
        public string TaxDetail { get; set; }

        [Range(0.00, double.MaxValue, ErrorMessage = "Please enter a positive price")]
        public decimal? ShippingCharge { get; set; }
        public string ShippingDetail { get; set; }

        [Range(0.00, double.MaxValue, ErrorMessage = "Please enter a positive price")]
        public decimal? OtherCharge { get; set; }
        public string OtherDetail { get; set; }


        [Range(0.00, double.MaxValue, ErrorMessage = "Please enter a positive price")]
        public decimal? Discount { get; set; }
        public string DiscountDetail { get; set; }

        public string Currency { get; set; }

        public string QuotationNo { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }

        public Order() {
            this.CreatedDate = DateTime.UtcNow;
            this.ModifiedDate = DateTime.UtcNow;
        }
    }
}
