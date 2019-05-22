using System;
using System.ComponentModel.DataAnnotations;

namespace RStore.Models {
    public class Setting {
        public int SettingID { get; set; }
        [Display(Name = "Site Title")]
        public string SiteTitle { get; set; }
        [Display(Name = "Site Description")]
        public string MetaDescription { get; set; }
        [Display(Name = "Site Keywords")]
        public string MetaKeywords { get; set; }
        [Display(Name = "Website Logo (796 * 125)")]
        public string Logo { get; set; }
        [Display(Name = "Admin Panel Logo (289 * 125)")]
        public string AdminLogo { get; set; }
        [Display(Name = "Close Website?")]
        public bool Closed { get; set; }
        [Display(Name = "Company Name")]
        public string CompanyName { get; set; }
        [Display(Name = "Reg No.")]
        public string RegNo { get; set; }
        [Display(Name = "Address")]
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        [Display(Name = "Phone No.")]
        public string PhoneNo { get; set; }
        [Display(Name = "Fax No.")]
        public string FaxNo { get; set; }
        [Display(Name = "Enquiry Email")]
        public string Email { get; set; }
        [Display(Name = "Administrator's Email")]
        public string AdminEmail { get; set; }
        [Display(Name = "Facebook Page ID (for messenger)")]
        public string FacebookPageID { get; set; }
        [Display(Name = "Facebook Page Url (for footer facebook page)")]
        public string FacebookPageUrl { get; set; }

        [Display(Name = "SMTP Server")]
        public string Host { get; set; }
        [Display(Name = "SMTP Port")]
        public int Port { get; set; }
        [Display(Name = "SMTP Enable SSL?")]
        public bool EnableSsl { get; set; }
        [Display(Name = "SMTP Sender Email")]
        public string FromEmail { get; set; }
        [Display(Name = "SMTP Sender Password")]
        public string Password { get; set; }

        [Display(Name = "Quotation Email Header")]
        public string EmailHeader { get; set; }
        [Display(Name = "Quotation Email Format")]
        public string EmailFormat { get; set; }
        [Display(Name = "Quotation Email Signature")]
        public string Signature { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }

        public Setting() {
            this.CreatedDate = DateTime.UtcNow;
            this.ModifiedDate = DateTime.UtcNow;
        }
    }
}
