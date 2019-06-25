using Abp.Configuration.Startup;
using Abp.Localization.Dictionaries;
using Abp.Localization.Dictionaries.Xml;
using Abp.Reflection.Extensions;

namespace GargleWool.Core.Localization
{
    public static class GargleWoolLocalizationConfigure
    {
        public static void Configure(ILocalizationConfiguration localizationConfiguration)
        {
            localizationConfiguration.Sources.Add(
                new DictionaryBasedLocalizationSource(GargleWoolConstants.LocalizationSourceName,
                    new XmlEmbeddedFileLocalizationDictionaryProvider(
                        typeof(GargleWoolLocalizationConfigure).GetAssembly(),
                "GargleWool.Core.Localization.SourceFiles")));
        }
    }
}