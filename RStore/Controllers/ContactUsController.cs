using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RStore.Infrastructure;
using RStore.Models;
using RStore.Models.ViewModels;

namespace RStore.Controllers {
    public class ContactUsController : Controller {
        private ISettingRepository repository;

        public ContactUsController(ISettingRepository repo) {
            repository = repo;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ServiceFilter(typeof(ValidateReCaptchaAttribute))]
        public async Task<IActionResult> Submit(ContactFormViewModel form, int? productId, int? pageId) {
            if (ModelState.IsValid) {
                var setting = repository.Setting;
                if (setting != null) {
                    var smtpClient = new SmtpClient {
                        Host = setting.Host, // set your SMTP server name here
                        Port = setting.Port, // Port 
                        EnableSsl = setting.EnableSsl,
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                        UseDefaultCredentials = false,// disable it
                        Credentials = new NetworkCredential(setting.FromEmail, Misc.DecodeBase64(setting.Password))
                    };

                    StringBuilder body = new StringBuilder()
                        .AppendLine("Hi,")
                        .AppendLine()
                        .AppendLine("A new enquiry has been submitted.");
                         
                    if (!string.IsNullOrEmpty(form.ProductInterested)) {
                        body.AppendLine()
                            .AppendLine("------------------------------")
                            .AppendLine("Product Interested:")
                            .AppendFormat("{0} x {1}", form.ProductInterested, form.Qty)
                            .AppendLine()
                            .AppendLine("Ship To: " + form.ShipToName)
                            .AppendLine("Address: " + form.ShipToAddress)
                            .AppendLine("------------------------------");
                    }

                    body.AppendLine()
                        .AppendLine("Customer Name: " + form.Name)
                        .AppendLine("Company      : " + form.Company)
                        .AppendLine("Phone No.    : " + form.ContactNo)
                        .AppendLine("Email        : " + form.Email)
                        .AppendLine("Message      : " + form.Message)
                        .AppendLine()
                        .AppendLine();

                    using (var message = new MailMessage(setting.Email, setting.AdminEmail) {
                        Subject = "New enquiry submitted!",
                        Body = body.ToString()
                    }) {
                        try {
                            await smtpClient.SendMailAsync(message);
                            return RedirectToAction("Completed", "Cart");
                        } catch (Exception ex) {
                            TempData["error"] = ex.Message;
                        }
                    }
                }
            } else {
                TempData["error"] = "The are something error in following fields: <br />";
                foreach (var modelState in ModelState.Values) {
                    foreach (var modelError in modelState.Errors) {
                        TempData["error"] += " <li>" + modelError.ErrorMessage + " </li>";
                    }
                }
            }

            if (productId != null) 
                return RedirectToAction("Index", "Product", new { Id = productId });
            if (pageId != null) 
                return RedirectToAction("Index", "Page", new { Id = pageId });
            else 
                return RedirectToAction("Error", "Error");
        }



    }
}