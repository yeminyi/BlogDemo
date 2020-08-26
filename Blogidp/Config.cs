// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4.Models;
using System.Collections.Generic;
using IdentityServer4;
using IdentityModel;
using Microsoft.Extensions.Configuration;

namespace BlogIdp
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new IdentityResource[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email()
            };
        }

        public static IEnumerable<ApiResource> GetApis()
        {
            return new ApiResource[]
  {
                new ApiResource("restapi", "My RESTful API", new List<string>{
                    "name",
                    "gender",
                    JwtClaimTypes.PreferredUserName,
                    JwtClaimTypes.Picture})
  };
        }

        public static IEnumerable<Client> GetClients(IConfiguration configuration)
        {
            return new[]
            {
                // MVC client using hybrid flow
                new Client
                {
                    ClientId = "mvcclient",
                    ClientName = "MVC Client",

                    AllowedGrantTypes = GrantTypes.HybridAndClientCredentials,
                    ClientSecrets = { new Secret("49C1A7E1-0C79-4A89-A3D6-A37998FB86B0".Sha256()) },
 
                    RedirectUris = { $"{configuration["MVCAddress"]}/signin-oidc" },
                    FrontChannelLogoutUri = $"{configuration["MVCAddress"]}/signout-oidc",
                    PostLogoutRedirectUris = { $"{configuration["MVCAddress"]}/signout-callback-oidc" },

                    AllowOfflineAccess = true, // offline_access
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        "restapi"
                    }
                },

                  // Angular client using implicit flow
                new Client
                {
                    ClientId = "blog-client",
                    ClientName = "Blog Client",
                    //ClientUri = "http://localhost:4200",
                    ClientUri =configuration["ClientAddress"],
            AllowedGrantTypes = GrantTypes.Implicit,
                    AllowAccessTokensViaBrowser = true,
                    RequireConsent = false,
                    AccessTokenLifetime = 180,
                   
                    RedirectUris =
                    {
                        $"{configuration["ClientAddress"]}/signin-oidc",
                         $"{configuration["ClientAddress"]}/redirect-silentrenew"
                    },

                    PostLogoutRedirectUris = { configuration["ClientAddress"] },
                    AllowedCorsOrigins = { configuration["ClientAddress"] },

                    AllowedScopes = {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        "restapi" }
                }
            };
        }
    }
}