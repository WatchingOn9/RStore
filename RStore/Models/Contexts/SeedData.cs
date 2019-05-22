using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace RStore.Models {
    public class SeedData {
        public static void EnsurePopulated(IApplicationBuilder app) {
            AppDbContext context = app.ApplicationServices
                .GetRequiredService<AppDbContext>();
            context.Database.Migrate();

            if (!context.Categories.Any()) {
                context.Categories.AddRange(
                    new Category { CategoryName = "WaterSports", IsMainCategory = true },
                    new Category { CategoryName = "Soccer", IsMainCategory = true },
                    new Category { CategoryName = "Chess", IsMainCategory = true }
                );
                context.SaveChanges();
            }

            if (!context.Products.Any()) {
                context.Products.AddRange(
                    new Product {
                        ItemCode = "10000000", Barcode = "1000000", Name = "Kayak", Description = "A boat for one person", Price = 275m,
                        Category = context.Categories.FirstOrDefault(x => x.CategoryName == "WaterSports"), Currency = "MYR"
                    },
                    new Product {
                        ItemCode = "10000000", Barcode = "1000000", Name = "Lifejacket", Description = "Protective and fashionable", Price = 48.95m,
                        Category = context.Categories.FirstOrDefault(x => x.CategoryName == "WaterSports"), Currency = "MYR"
                    },
                    new Product {
                        ItemCode = "10000000", Barcode = "1000000", Name = "Soccer Ball", Description = "FIFA-approved size and weight", Price = 19.50m,
                        Category = context.Categories.FirstOrDefault(x => x.CategoryName == "Soccer"), Currency = "MYR"
                    },
                    new Product {
                        ItemCode = "10000000", Barcode = "1000000", Name = "Corner Flags", Description = "Give your playing field a professional touch", Price = 34.95m,
                        Category = context.Categories.FirstOrDefault(x => x.CategoryName == "Soccer"), Currency = "MYR"
                    },
                    new Product {
                        ItemCode = "10000000", Barcode = "1000000", Name = "Stadium", Description = "Flat-packed 35,000-seat stadium", Price = 79500m,
                        Category = context.Categories.FirstOrDefault(x => x.CategoryName == "Soccer"), Currency = "MYR"
                    },
                    new Product {
                        ItemCode = "10000000", Barcode = "1000000", Name = "Thinking Cap", Description = "Improve brain efficiency by 75%", Price = 16m,
                        Category = context.Categories.FirstOrDefault(x => x.CategoryName == "Chess"), Currency = "MYR"
                    },
                    new Product {
                        ItemCode = "10000000", Barcode = "1000000", Name = "Unsteady Chair", Description = "Secretly give your opponent a disadvantage", Price = 29.95m,
                        Category = context.Categories.FirstOrDefault(x => x.CategoryName == "Chess"), Currency = "MYR"
                    },
                    new Product {
                        ItemCode = "10000000", Barcode = "1000000", Name = "Human Chess Board", Description = "A fun game for family", Currency = "MYR",
                        Category = context.Categories.FirstOrDefault(x => x.CategoryName == "Chess"), Price = 75m
                    },
                    new Product {
                        ItemCode = "10000000", Barcode = "1000000", Name = "Bling-Bling King", Description = "Gold-plated, diamond-studded King",
                        Category = context.Categories.FirstOrDefault(x => x.CategoryName == "Chess"), Price = 1200m, Currency = "MYR"
                    }
                );
                context.SaveChanges();
            }

            if (!context.Settings.Any()) {
                context.Settings.AddRange(
                    new Setting { SiteTitle = "Hello" }
                );
                context.SaveChanges();
            }

            if (!context.Pages.Any()) {
                context.Pages.AddRange(
                    new Page { PageName = "Home", Title = "首页<br />Home", ShowProduct = true, Order = 0 },
                    new Page { PageName = "About Us", Title = "关于我们<br /> About Us", Order = 1 },
                    new Page { PageName = "Products", Title = "产品 <br />Products", ShowProduct = true, Order = 2 },
                    new Page { PageName = "Contact Us", Title = "联系我们 <br />Contact Us", ShowProduct = false, Order = 3 }
                );
                context.SaveChanges();
            }
        }
    }
}
