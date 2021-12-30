using Microsoft.Extensions.Caching.Memory;
using OnionAppTraining.Infrastructure.DTO;
using System;

namespace OnionAppTraining.Infrastructure.Extensions
{
    public static class CacheExtensions
    {
        public static void SetJwt(this IMemoryCache memoryCache, Guid tokenId, JwtDTO jwtTDO)
            => memoryCache.Set(GetJwtKey(tokenId), jwtTDO, TimeSpan.FromSeconds(5));

        public static JwtDTO GetJwt(this IMemoryCache memoryCache, Guid tokenId)
            => memoryCache.Get<JwtDTO>(GetJwtKey(tokenId));

        private static string GetJwtKey(Guid tokenId) => $"jwt-{tokenId}";
    }
}
