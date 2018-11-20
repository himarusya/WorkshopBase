using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorshopBase.Services;
using WorshopBase.ViewModels;

namespace WorshopBase.Middleware
{
    public class DbCacheMiddleware
    {
        private readonly RequestDelegate requestDelegate;
        private readonly IMemoryCache memoryCache;
        private string cacheKey;

        public DbCacheMiddleware(RequestDelegate requestDelegate, IMemoryCache memoryCache, 
            string cacheKey = "Workshop")
        {
            try
            {
                this.requestDelegate = requestDelegate;
                this.memoryCache = memoryCache;
                this.cacheKey = cacheKey;
            }
            catch (Exception ex)
            {

            }
        }

        public Task Invoke(HttpContext httpContext, WorkshopService service)
        {
            try
            {
                HomeViewModel model;
                if (!memoryCache.TryGetValue(cacheKey, out model))
                {
                    model = service.GetHomeViewModel();

                    memoryCache.Set(cacheKey, model,
                        new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(5)));
                }
            }
            catch(Exception ex)
            {

            }
            return requestDelegate(httpContext);
        }
    }
}
