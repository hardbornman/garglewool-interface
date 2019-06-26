using System.Linq;
using Abp.Configuration;
using Abp.Localization;
using Abp.Net.Mail;
using Microsoft.EntityFrameworkCore;

namespace GargleWool.EntityFrameworkCore.EntityFrameworkCore.Seed.Host
{
    public class DefaultSettingsCreator
    {

        private readonly GargleWoolDbContext _context;

        public DefaultSettingsCreator(GargleWoolDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            // Emailing
            AddSettingIfNotExists(EmailSettingNames.DefaultFromAddress, "1021663187@qq.com");
            AddSettingIfNotExists(EmailSettingNames.DefaultFromDisplayName, "zhutoutou");

            // Languages
            AddSettingIfNotExists(LocalizationSettingNames.DefaultLanguage, "zh-Hans");
        }

        private void AddSettingIfNotExists(string name, string value, int? tenantId = null)
        {
            if (_context.Settings.IgnoreQueryFilters().Any(s => s.Name == name && s.TenantId == tenantId && s.UserId == null))
            {
                return;
            }

            _context.Settings.Add(new Setting(tenantId, null, name, value));
            _context.SaveChanges();
        }
    }
}