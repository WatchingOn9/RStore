using System.Linq;

namespace RStore.Models {
    public interface IComponentRepository {
        IQueryable<Component> Components { get; }
        void SaveComponent(Component component);
        Component DeleteComponent(int componentID);
    }
}
