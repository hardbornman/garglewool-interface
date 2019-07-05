using Abp.AspNetCore.Mvc.Controllers;
using Abp.IdentityFramework;
using GargleWool.Core;
using Microsoft.AspNetCore.Identity;

namespace GargleWool.Web.Core.Controllers
{
    public class GargleWoolControllerBase : AbpController
    {
        protected GargleWoolControllerBase()
        {
            LocalizationSourceName = GargleWoolConstants.LocalizationSourceName;
        }

        protected void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}