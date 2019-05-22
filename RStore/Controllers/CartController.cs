using System;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RStore.Infrastructure;
using RStore.Models;
using RStore.Models.ViewModels;

namespace RStore.Controllers {
    public class CartController : Controller {
        private IProductRepository repository;
        private IOrderRepository oRepo;
        private ISettingRepository sRepo;
        private Cart cart;

        public CartController(IProductRepository repo, IOrderRepository repoService, Cart cartService, ISettingRepository settRepo) {
            repository = repo;
            oRepo = repoService;
            sRepo = settRepo;
            cart = cartService;
        }

        public ViewResult Index(string returnUrl) {
            return View(new CartIndexViewModel {
                Cart = cart,
                Order = new Order(),
                ReturnUrl = returnUrl
            });
        }

        public RedirectToActionResult AddToCart(int productId, string returnUrl, int qty = 1) {
            Product product = repository.Products
                .FirstOrDefault(p => p.ProductID == productId);

            if (product != null) {
                cart.AddItem(product, qty);
            }

            return RedirectToAction("Index", new { returnUrl });
        }

        public RedirectToActionResult UpdateQty(int productId, string returnUrl, int qty = 1) {
            Product product = repository.Products
                .FirstOrDefault(p => p.ProductID == productId);

            if (product != null) {
                cart.UpdateQty(product, qty);
                TempData["message"] = $"{product.Name} has been updated.";
            }

            return RedirectToAction("Index", new { returnUrl });
        }

        public RedirectToActionResult RemoveFromCart(int productId, string returnUrl) {
            Product product = repository.Products
                .FirstOrDefault(p => p.ProductID == productId);

            if (product != null) {
                cart.RemoveLine(product);
            }

            return RedirectToAction("Index", new { returnUrl });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ServiceFilter(typeof(ValidateReCaptchaAttribute))]
        public async Task<IActionResult> Checkout(CartIndexViewModel model) {
            if (cart.Lines.Count() == 0) {
                ModelState.AddModelError("", "Sorry, your cart is empty!");
            }
            if (ModelState.IsValid) {
                model.Order.Lines = cart.Lines.ToArray();
                model.Order.Currency = model.Order.Lines.FirstOrDefault().Product.Currency;
                oRepo.SaveOrder(model.Order);
                bool isSent = await SendMail();
                return RedirectToAction(nameof(Completed));
            } else {
                TempData["error"] = "Some Fields is invalid.";
                model.Cart = cart;
                return View("Index", model);
            }
        }

        public ViewResult Completed() {
            cart.Clear();
            return View();
        }

        private async Task<bool> SendMail() {
            var setting = sRepo.Setting;
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
                    .AppendLine("Dear administator,")
                    .AppendLine()
                    .AppendLine("A new quotation request has been placed.")
                    .AppendLine()
                    .AppendLine("Please login to backend: " + $"https://{this.Request.Host}{this.Request.PathBase}/admin/index to check out.")
                    .AppendLine()
                    .AppendLine("From " + setting.SiteTitle + "'s Website");

                using (var message = new MailMessage(setting.FromEmail, setting.AdminEmail) {
                    Subject = "A New Quotation Request Has Been Placed",
                    Body = body.ToString()
                }) {
                    try {
                        await smtpClient.SendMailAsync(message);
                    } catch (Exception ex) {
                        return false;
                    }
                }
            }

            return true;
        }

    }
}