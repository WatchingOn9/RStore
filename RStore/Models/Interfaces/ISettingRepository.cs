using System.Linq;

namespace RStore.Models {
    public interface ISettingRepository {
        Setting Setting { get; }
        void SaveSetting(Setting setting);
    }
}

