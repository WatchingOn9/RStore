using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RStore.Models {
    public class EFProductRepository : IProductRepository {
        private AppDbContext context;

        public EFProductRepository(AppDbContext ctx) {
            context = ctx;
        }

        public IQueryable<Product> Products => context.Products.Include(o => o.Images).Include(x => x.Category);

        public void SaveProduct(Product product) {
            context.AttachRange(product.Images);
            if (product.ProductID == 0) {
                context.Products.Add(product);
            } else {
                Product dbEntry = context.Products
                    .FirstOrDefault(p => p.ProductID == product.ProductID);
                if (dbEntry != null) {
                    dbEntry.ItemCode = product.ItemCode;
                    dbEntry.Barcode = product.Barcode;
                    dbEntry.Name = product.Name;
                    dbEntry.Description = product.Description;
                    dbEntry.Currency = product.Currency;
                    dbEntry.Price = product.Price;
                    dbEntry.Category = product.Category;
                    dbEntry.CategoryID = product.CategoryID;
                    dbEntry.ThumbImage = product.ThumbImage;
                    dbEntry.Disable = product.Disable;
                    dbEntry.UOM = product.UOM;
                    dbEntry.ModifiedDate = DateTime.UtcNow;

                    foreach (var image in product.Images) {
                        if (!dbEntry.Images.Any(p => p.ImageName == image.ImageName))
                            dbEntry.AddImage(image.ImageName);
                    }
                }
            }
            context.SaveChanges();
        }

        public Product DeleteProduct(int productID) {
            Product dbEntry = context.Products
                .Include(x => x.Images)
                .FirstOrDefault(p => p.ProductID == productID);
            if (dbEntry != null) {
                context.Products.Remove(dbEntry);

                if (dbEntry.Images != null) {
                    context.Images.RemoveRange(dbEntry.Images);
                }

                context.SaveChanges();
            }
            return dbEntry;
        }

        public IEnumerable<string> RemoveAllImages(int productID) {
            Product dbEntry = context.Products
                .Include(x => x.Images)
                .FirstOrDefault(p => p.ProductID == productID);
            var images = new List<string>();
            if (dbEntry != null) {
                if (dbEntry.Images != null) {
                    foreach (var img in dbEntry.Images) {
                        images.Add(img.ImageName);
                    }
                    context.Images.RemoveRange(dbEntry.Images);
                }

                context.SaveChanges();
            }
            return images;
        }
    }
    
}
