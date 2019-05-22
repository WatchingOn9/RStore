using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace RStore.Models {
    public class EFPageRepository : IPageRepository {
        private AppDbContext context;

        public EFPageRepository(AppDbContext ctx) {
            context = ctx;
        }

        public IQueryable<Page> Pages => context.Pages.Include(x => x.ParentPage);

        public void SavePage(Page page) {
            if (page.PageID == 0) {
                context.Pages.Add(page);
            } else {
                Page dbEntry = context.Pages
                    .FirstOrDefault(p => p.PageID == page.PageID);
                if (dbEntry != null) {
                    dbEntry.Content = page.Content;
                    dbEntry.Disable = page.Disable;
                    dbEntry.Order = page.Order;
                    dbEntry.PageName = page.PageName;
                    dbEntry.ParentID = page.ParentID;
                    dbEntry.ParentPage = page.ParentPage;
                    dbEntry.ShowProduct = page.ShowProduct;
                    dbEntry.Title = page.Title;
                    dbEntry.ShowEnquiryForm = page.ShowEnquiryForm;
                    dbEntry.ModifiedDate = DateTime.UtcNow;
                }
            }
            context.SaveChanges();
        }

        public Page DeletePage(int pageID) {
            Page dbEntry = context.Pages
                .FirstOrDefault(p => p.PageID == pageID);
            if (dbEntry != null) {
                context.Pages.Remove(dbEntry);
                context.SaveChangesAsync();
            }
            return dbEntry;
        }
    }
}
