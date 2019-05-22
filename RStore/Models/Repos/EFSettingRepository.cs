using Microsoft.EntityFrameworkCore;
using RStore.Infrastructure;
using System;
using System.Linq;

namespace RStore.Models {
    public class EFSettingRepository : ISettingRepository {
        private AppDbContext context;

        public EFSettingRepository(AppDbContext ctx) {
            context = ctx;
        }

        public Setting Setting => context.Settings.FirstOrDefault();

        public void SaveSetting(Setting setting) {
            if (setting.SettingID == 0) {
                context.Settings.Add(setting);
            } else {
                Setting dbEntry = context.Settings
                    .FirstOrDefault(p => p.SettingID == setting.SettingID);
                if (dbEntry != null) {
                    dbEntry.CompanyName = setting.CompanyName;
                    dbEntry.Address1 = setting.Address1;
                    dbEntry.Address2 = setting.Address2;
                    dbEntry.Address3 = setting.Address3;
                    dbEntry.Closed = setting.Closed;
                    dbEntry.Email = setting.Email;
                    dbEntry.AdminEmail = setting.AdminEmail;
                    dbEntry.FacebookPageID = setting.FacebookPageID;
                    dbEntry.FacebookPageUrl = setting.FacebookPageUrl;
                    dbEntry.FaxNo = setting.FaxNo;
                    dbEntry.MetaDescription = setting.MetaDescription;
                    dbEntry.MetaKeywords = setting.MetaKeywords;
                    dbEntry.PhoneNo = setting.PhoneNo;
                    dbEntry.RegNo = setting.RegNo;
                    dbEntry.SiteTitle = setting.SiteTitle;
                    dbEntry.Logo = setting.Logo;
                    dbEntry.AdminLogo = setting.AdminLogo;
                    dbEntry.Host = setting.Host;
                    dbEntry.Port = setting.Port;
                    dbEntry.EnableSsl = setting.EnableSsl;
                    dbEntry.FromEmail = setting.FromEmail;
                    if (dbEntry.Password != setting.Password) {
                        dbEntry.Password = Misc.EncodeBase64(setting.Password);
                    }
                    dbEntry.EmailFormat = setting.EmailFormat;
                    dbEntry.EmailHeader = setting.EmailHeader;
                    dbEntry.Signature = setting.Signature;
                    dbEntry.ModifiedDate = DateTime.UtcNow;
                }
            }
            context.SaveChanges();
        }
    }
}
