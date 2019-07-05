using System.Collections;
using System.Collections.Generic;
using GargleWool.Core;
using IdentityModel;
using IdentityServer4.Models;

namespace ServiceOAuth.Configuration
{
    public class InMemoryConfiguration
    {
        public static IEnumerable<ApiResource> ApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource("api1","ServiceA api")
                {
                    UserClaims = new List<string>{"City","State"}
                },
                new ApiResource(GargleWoolConstants.ScopeApi,"GargleWool (all) API")
            };
        }

        public static IEnumerable<IdentityResource> IdentityResources()
        {
            return new List<IdentityResource>()
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email(),
                new IdentityResources.Phone(),
            };
        }

        public static IEnumerable<Client> Clients()
        {
            return new[]
            {
                new Client
                {
                    ClientId = "default.client",
                    ClientSecrets =
                    {
                        new Secret("defaultSecret".Sha256())
                    },
                    AllowedScopes =
                    {
                        "api1"
                    },
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    AccessTokenType = AccessTokenType.Jwt,
                    AlwaysSendClientClaims = true
                },
                new Client
                {
                    ClientId = "garglewool.client",
                    ClientSecrets =
                    {
                        new Secret("GargleWool".Sha256())
                    },
                    AllowedScopes =
                    {
                        "api1",
                        "garglewool-api"
                    },
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    AccessTokenType = AccessTokenType.Jwt,
                    AlwaysSendClientClaims = true
                }
            };
        }
    }
}