﻿using Abp.AutoMapper;
using GargleWool.Web.Core.Authentication.External;

namespace GargleWool.Web.Core.Models.TokenAuth
{
    [AutoMapFrom(typeof(ExternalLoginProviderInfo))]
    public class ExternalLoginProviderInfoModel
    {
        public string Name { get; set; }

        public string ClientId { get; set; }
    }
}