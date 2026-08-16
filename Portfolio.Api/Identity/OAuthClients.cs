using System;
using System.Collections.Generic;

namespace Portfolio.Api.Identity
{
    // Eski IdentityServer4 Config.cs'deki Clients tanimlarinin basit JWT karsiligi.
    // Ayni client_id/client_secret/scope degerleri korunuyor ki Angular tarafinda
    // (auth.service.ts) hic degisiklik gerekmesin.
    public static class OAuthClients
    {
        public const string PublicClientId = "SelinOzogluUI";
        public const string PublicClientSecret = "4sxQ54123!1x8Ss23.?";
        public static readonly string[] PublicScopes = { "selin.ozoglu.com.work.read" };
        public static readonly TimeSpan PublicAccessTokenLifetime = TimeSpan.FromHours(24);

        public const string AdminClientId = "SelinOzogluUIAdminPanel";
        public const string AdminClientSecret = "7sxQ54123!.19DSs23";
        public static readonly string[] AdminScopes = { "selin.ozoglu.com.work.read", "selin.ozoglu.com.work.write" };
        public static readonly TimeSpan AdminAccessTokenLifetime = TimeSpan.FromHours(48);
        public static readonly TimeSpan AdminRefreshTokenLifetime = TimeSpan.FromDays(60);

        public static bool IsValidPublicClient(string clientId, string clientSecret) =>
            clientId == PublicClientId && clientSecret == PublicClientSecret;

        public static bool IsValidAdminClient(string clientId, string clientSecret) =>
            clientId == AdminClientId && clientSecret == AdminClientSecret;
    }
}
