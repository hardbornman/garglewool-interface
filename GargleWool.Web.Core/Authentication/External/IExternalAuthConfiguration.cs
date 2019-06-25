using System.Collections.Generic;

namespace GargleWool.Web.Core.Authentication.External
{
    public interface IExternalAuthConfiguration
    {
        List<ExternalLoginProviderInfo> Providers { get; }
    }
}