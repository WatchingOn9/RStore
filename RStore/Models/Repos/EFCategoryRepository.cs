using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace RStore.Models {
    public class EFCategoryRepository : ICategoryRepository {
        private AppDbContext context;

        public EFCategoryRepository(AppDbContext ctx) {
            context = ctx;
        }

        public IQueryable<Category> Categories => context.Categories.Include(x => x.ParentCategory).Include(x => x.Childs);

        public void SaveCategory(Category category) {
            context.AttachRange(category.Childs);
            if (category.CategoryID == 0) {
                context.Categories.Add(category);
            } else {
                Category dbEntry = context.Categories
                    .FirstOrDefault(p => p.CategoryID == category.CategoryID);
                if (dbEntry != null) {
                    dbEntry.CategoryName = category.CategoryName;
                    dbEntry.Description = category.Description;
                    dbEntry.Disable = category.Disable;
                    dbEntry.IsMainCategory = category.IsMainCategory;
                    dbEntry.Order = category.Order;
                    dbEntry.ParentCategory = category.ParentCategory;
                    dbEntry.ParentID = category.ParentID;
                    dbEntry.ModifiedDate = DateTime.UtcNow;
                }
            }
            context.SaveChanges();
        }

        public Category DeleteCategory(int categoryID) {
            Category dbEntry = context.Categories
                .FirstOrDefault(p => p.CategoryID == categoryID);
            if (dbEntry != null) {
                context.Categories.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }
    }
}
