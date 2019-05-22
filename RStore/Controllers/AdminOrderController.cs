using Microsoft.AspNetCore.Mvc;
using RStore.Models;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using RStore.Models.ViewModels;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Net;
using System.Text;
using System;
using RStore.Infrastructure;

namespace RStore.Controllers {
    [Authorize]
    public class AdminOrderController : Controller {
        private IOrderRepository repository;
        private ISettingRepository sRepo;
        public int PageSize = 10;

        public AdminOrderController(IOrderRepository repoService, ISettingRepository settingRepo) {
            repository = repoService;
            sRepo = settingRepo;
        }

        public ViewResult Index(int page = 1) {
            return View(new OrderListViewModel {
                Orders = repository.Orders.Where(o => !o.Cancelled)
                .OrderByDescending(p => p.CreatedDate)
                    .Skip((page - 1) * PageSize)
                    .Take(PageSize),
                PagingInfo = new PagingInfo {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = repository.Orders.Count(o => !o.Cancelled)
                }
            });
        }

        public IActionResult ViewDetail(int Id) {
            var order = repository.Orders.FirstOrDefault(o => o.OrderID == Id);
            return View(new OrderEditViewModel {
                Order = order,
                Lines = order.Lines.ToList()
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ViewDetail(OrderEditViewModel model) {
            if (ModelState.IsValid) {
                model.Order.Lines = model.Lines;
                repository.SaveOrder(model.Order);
            } else {
                TempData["error"] = "Some Fields is invalid.";
            }

            return RedirectToAction("Index", new { Id = model.Order.OrderID });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult MarkSent(int orderID) {
            Order order = repository.Orders
                .FirstOrDefault(o => o.OrderID == orderID);
            if (order != null) {
                order.Sent = true;
                repository.SaveOrder(order);
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult MarkCancelled(int orderID) {
            Order order = repository.Orders
                .FirstOrDefault(o => o.OrderID == orderID);
            if (order != null) {
                order.Cancelled = true;
                repository.SaveOrder(order);
            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> SendMail(int orderID) {
            var setting = sRepo.Setting;
            var order = repository.Orders.FirstOrDefault(o => o.OrderID == orderID);
            if (setting != null && order != null) {
                var smtpClient = new SmtpClient {
                    Host = setting.Host, // set your SMTP server name here
                    Port = setting.Port, // Port 
                    EnableSsl = setting.EnableSsl,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,// disable it
                    Credentials = new NetworkCredential(setting.FromEmail, Misc.DecodeBase64(setting.Password))
                };

                string html = setting.EmailHeader + setting.EmailFormat + setting.Signature;
                html = replaceWildCards(html, order);

                using (var message = new MailMessage(setting.FromEmail, order.Email) {
                    Subject = "Quotation From " + setting.CompanyName,
                    IsBodyHtml = true,
                    Body = html
                }) {
                    try {
                        await smtpClient.SendMailAsync(message);
                        order.Sent = true;
                        repository.SaveOrder(order);
                        TempData["message"] = "Done..";
                    } catch (Exception ex) {
                        TempData["error"] = ex.Message;
                    }
                }
            }

            return RedirectToAction(nameof(Index));
        }

        private string replaceWildCards(string html, Order order) {
            var total = 0M;
            var address = order.Line1 + " " + order.Line2 + " " + order.Line3 + " " + order.Zip + " " + order.City + ", " + order.State + " " + order.Country + ".";
            var item = "<table border=\"1\" cellspacing=\"0\" cellpadding=\"0\" style=\"border: 1px solid #000; width: 100%\">"
                + "<tr>"
                + "<th>Product Code</th>"
                + "<th>Product Name</th>"
                + "<th>UOM</th>"
                + "<th>Quantity</th>"
                + "<th>Unit Price (" + order.Currency + ")</th>"
                + "<th>SubTotal (" + order.Currency + ")</th>"
                + "</tr>";

            foreach (var line in order.Lines) {
                if (line.Quantity > 0) {
                    item += "<tr>"
                    + "<td>" + line.Product.ItemCode + "</td>"
                    + "<td>" + line.Product.Name + "</td>"
                    + "<td>" + line.Product.UOM + "</td>"
                    + "<td style=\"text-align: center\">" + line.Quantity + "</td>"
                    + "<td style=\"text-align: right\">" + line.Price.ToString("#,##0.00") + "</td>"
                    + "<td style=\"text-align: right\">" + (line.Price * line.Quantity).ToString("#,##0.00") + "</td>"
                    + "</tr>";
                    total += line.Price * line.Quantity;
                }
            }
            if (!string.IsNullOrEmpty(order.FreeGift)) {
                item += "<tr>"
                    + "<td colspan=\"5\">Free: " + order.FreeGift + "</td>"
                    + "<td style=\"text-align: right\">0.00</td>"
                    + "</tr>";
                total -= order.Discount ?? 0;
            }
            if (order.Discount > 0) {
                item += "<tr>"
                    + "<td colspan=\"5\">Discount: " + order.DiscountDetail + "</td>"
                    + "<td style=\"text-align: right\">" + ((order.Discount ?? 0) * -1).ToString("#,##0.00") + "</td>"
                    + "</tr>";
                total -= order.Discount ?? 0;
            }
            if (order.ShippingCharge > 0) {
                item += "<tr>"
                    + "<td colspan=\"5\">Shipping: " + order.ShippingDetail + "</td>"
                    + "<td style=\"text-align: right\">" + (order.ShippingCharge ?? 0).ToString("#,##0.00") + "</td>"
                    + "</tr>";
                total += order.ShippingCharge ?? 0;
            }
            if (order.Tax > 0) {
                item += "<tr>"
                    + "<td colspan=\"5\">Tax: " + order.TaxDetail + "</td>"
                    + "<td style=\"text-align: right\">" + (order.Tax ?? 0).ToString("#,##0.00") + "</td>"
                    + "</tr>";
                total += order.Tax ?? 0;
            }
            if (order.OtherCharge > 0) {
                item += "<tr>"
                    + "<td colspan=\"5\">Other: " + order.OtherDetail + "</td>"
                    + "<td style=\"text-align: right\">" + (order.OtherCharge ?? 0).ToString("#,##0.00") + "</td>"
                    + "</tr>";
                total += order.OtherCharge ?? 0;
            }

            item += "<tfoot><tr>"
                + "<td colspan=\"5\"><b>Total</b></td>"
                + "<td style=\"text-align: right\"><b>" + total.ToString("#,##0.00") + "</b></td>"
                + "</tr></tfoot></table>";

            html = html.Replace("*QuotationNo*", order.QuotationNo)
                    .Replace("*TodayDate*", DateTime.Today.ToString("yyyy/MM/dd"))
                    .Replace("*CompanyName*", order.CompanyName)
                    .Replace("*ContactPerson*", order.ContactPerson)
                    .Replace("*PhoneNo*", order.PhoneNo)
                    .Replace("*Email*", order.Email)
                    .Replace("*Address*", address)
                    .Replace("*CustomerRemark*", order.ContactPerson)
                    .Replace("*Items*", item);
            return html;
        }
    }
}