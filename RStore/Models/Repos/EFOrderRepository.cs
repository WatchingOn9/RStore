using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RStore.Models {
    public class EFOrderRepository : IOrderRepository {
        private AppDbContext context;

        public EFOrderRepository(AppDbContext ctx) {
            context = ctx;
        }

        public IQueryable<Order> Orders => context.Orders.Include(o => o.Lines).ThenInclude(l => l.Product);

        public void SaveOrder(Order order) {
            context.AttachRange(order.Lines.Select(l => l.Product));
            if (order.OrderID == 0) {
                context.Orders.Add(order);
            } else {
                Order dbEntry = context.Orders.Include(o => o.Lines).FirstOrDefault(p => p.OrderID == order.OrderID);
                if (dbEntry != null) {
                    List<CartLine> lines = dbEntry.Lines.ToList();

                    dbEntry.Currency = order.Currency;
                    dbEntry.DiscountDetail = order.DiscountDetail;
                    dbEntry.Discount = order.Discount;
                    dbEntry.ShippingDetail = order.ShippingDetail;
                    dbEntry.ShippingCharge = order.ShippingCharge;
                    dbEntry.Tax = order.Tax;
                    dbEntry.TaxDetail = order.TaxDetail;
                    dbEntry.OtherCharge = order.OtherCharge;
                    dbEntry.OtherDetail = order.OtherDetail;
                    dbEntry.CompanyName = order.CompanyName;
                    dbEntry.ContactPerson = order.ContactPerson;
                    dbEntry.CompanyName = order.CompanyName;
                    dbEntry.ContactPerson = order.ContactPerson;
                    dbEntry.Email = order.Email;
                    dbEntry.PhoneNo = order.PhoneNo;
                    dbEntry.Line1 = order.Line1;
                    dbEntry.Line2 = order.Line2;
                    dbEntry.Line3 = order.Line3;
                    dbEntry.Zip = order.Zip;
                    dbEntry.City = order.City;
                    dbEntry.State = order.State;
                    dbEntry.Country = order.Country;
                    dbEntry.CustomerRemark = order.CustomerRemark;
                    dbEntry.FreeGift = order.FreeGift;
                    dbEntry.QuotationNo = order.QuotationNo;
                    dbEntry.ModifiedDate = DateTime.Now;

                    foreach(var line in order.Lines) {
                        var oriLine = lines.FirstOrDefault(x => x.CartLineID == line.CartLineID);
                        if (oriLine != null) {
                            oriLine.Quantity = line.Quantity;
                            oriLine.Price = line.Price;
                        }
                    }
                }
            }
            context.SaveChanges();
        }
    }
}
