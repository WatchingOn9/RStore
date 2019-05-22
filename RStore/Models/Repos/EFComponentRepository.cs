using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RStore.Models {
    public class EFComponentRepository : IComponentRepository {
        private AppDbContext context;

        public EFComponentRepository(AppDbContext ctx) {
            context = ctx;
        } 

        public IQueryable<Component> Components => context.Components;

        public Component DeleteComponent(int componentID) {
            Component dbEntry = context.Components
                .FirstOrDefault(p => p.ComponentID == componentID);
            if (dbEntry != null) {
                context.Components.Remove(dbEntry);
                context.SaveChangesAsync();
            }
            return dbEntry;
        }

        public void SaveComponent(Component component) {
            if (component.ComponentID == 0) {
                context.Components.Add(component);
            } else {
                Component dbEntry = context.Components
                    .FirstOrDefault(p => p.ComponentID == component.ComponentID);
                if (dbEntry != null) {
                    dbEntry.Content = component.Content;
                    dbEntry.Disable = component.Disable;
                    dbEntry.Name = component.Name;
                    dbEntry.Order = component.Order;
                    dbEntry.Type = component.Type;
                    dbEntry.ModifiedDate = DateTime.UtcNow;
                }
            }
            context.SaveChanges();
        }
    }
}
