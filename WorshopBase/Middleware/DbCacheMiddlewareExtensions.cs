using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorshopBase.Middleware
{
    public static class DbCacheMiddlewareExtensions
    {
        public static IApplicationBuilder UseWorkshopCache(this IApplicationBuilder builder, string cacheKey)
        {
            return builder.UseMiddleware<DbCacheMiddleware>(cacheKey);
        }
    }
}
