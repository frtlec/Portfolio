// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4;
using IdentityServer4.Models;
using System;
using System.Collections.Generic;

namespace Portfolio.IdentityServer
{
  public static class Config
  {
    public static IEnumerable<ApiResource> ApiResources => new ApiResource[] {
            new ApiResource("resource_workitem"){Scopes={"selin.ozoglu.com.work.read","selin.ozoglu.com.work.write"}},
            new ApiResource("resource_photostock"){Scopes={"selin.ozoglu.com.work.read","selin.ozoglu.com.work.write"}},
            new ApiResource("resource_mailsender"){Scopes={"selin.ozoglu.com.work.read","selin.ozoglu.com.work.write"}},
            new ApiResource("resource_gateway"){Scopes={"selin.ozoglu.com.work.read","selin.ozoglu.com.work.write"}},
           new ApiResource(IdentityServerConstants.LocalApi.ScopeName)
        };
    public static IEnumerable<IdentityResource> IdentityResources =>
               new IdentityResource[]
               {
                       new IdentityResources.Email(),
                       new IdentityResources.OpenId(),
                       new IdentityResources.Profile(),
                        new IdentityResource(){Name="roles",DisplayName="Roles",Description="Kullanıcı Rolleri",UserClaims=new[]{"role"} }
               };

    public static IEnumerable<ApiScope> ApiScopes =>
        new ApiScope[]
        {
                    new ApiScope("selin.ozoglu.com.work.read","okuma yetkisi"),
                    new ApiScope("selin.ozoglu.com.work.write","yazma yetkisi"),
                new ApiScope(IdentityServerConstants.LocalApi.ScopeName)
        };

    public static IEnumerable<Client> Clients =>
        new Client[]
        {
                // m2m client credentials flow client
                 new Client
                {
                    ClientName="Selin Ozoglu Angular ",
                    ClientId="SelinOzogluUI",
                    ClientSecrets= {new Secret("VkYp3s6v9y$B&E)H@McQfTjWmZq4t7w!z%C*F-JaNdRgUkXp2r5u8x/A?D(GKbP".Sha256())},
                    AllowedGrantTypes= GrantTypes.ClientCredentials,
                    AllowedScopes={
                     "selin.ozoglu.com.work.read"
                   },
                    AccessTokenLifetime=60
                },
                new Client
                {
                   ClientName="Selin Ozoglu Angular Admin Panel",
                    ClientId="SelinOzogluUIAdminPanel",
                    AllowOfflineAccess=true,
                    ClientSecrets= {new Secret("t7w!z%C*F-JaNdRgUkXp2r5u8x/A?D(G+KbPeShVmYq3t6v9y$B&E)H@McQfTjWn".Sha256())},
                    AllowedGrantTypes= GrantTypes.ResourceOwnerPassword,
                    AllowedScopes={ "selin.ozoglu.com.work.read", "selin.ozoglu.com.work.write", IdentityServerConstants.StandardScopes.Email, IdentityServerConstants.StandardScopes.OpenId,IdentityServerConstants.StandardScopes.Profile, IdentityServerConstants.StandardScopes.OfflineAccess, IdentityServerConstants.LocalApi.ScopeName,"roles" },
                    AccessTokenLifetime=1*60*60,
                    RefreshTokenExpiration=TokenExpiration.Absolute,
                    AbsoluteRefreshTokenLifetime= (int) (DateTime.Now.AddDays(60)- DateTime.Now).TotalSeconds,
                    RefreshTokenUsage= TokenUsage.ReUse
                }
        };
  }
}