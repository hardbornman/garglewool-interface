using System.Threading.Tasks;
using Abp.Dependency;

namespace GargleWool.Web.Core.Authentication.External
{
    public abstract class ExternalAuthProviderApiBase:IExternalAuthProviderApi,ITransientDependency
    {
        public ExternalLoginProviderInfo ProviderInfo { get; set; }

        public async Task<bool> IsValidUser(string userId, string accessCode)
        {
            var userInfo = await GetUserInfo(accessCode);
            return userInfo.ProviderKey == userId;
        }

        public abstract Task<ExternalAuthUserInfo> GetUserInfo(string accessCode);

        public void Initialize(ExternalLoginProviderInfo providerInfo)
        {
            ProviderInfo = providerInfo;
        }
    }
}