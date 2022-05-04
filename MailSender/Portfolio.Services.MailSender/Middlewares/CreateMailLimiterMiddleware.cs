using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using System;
using System.Net;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Portfolio.Services.MailSender.Middlewares
{
  public class CreateMailLimiterMiddleware
  {
    private readonly RequestDelegate _next;
    private readonly IMemoryCache _memoryCache;
    private readonly IConfiguration _configuration;

    public CreateMailLimiterMiddleware(RequestDelegate next, IMemoryCache memoryCache, IConfiguration configuration)
    {
      _next = next;
      _memoryCache = memoryCache;
      _configuration = configuration;
    }

    public async Task Invoke(HttpContext context)
    {
      if (context.Request.Path.Value.IndexOf("/api/contacts/create", StringComparison.OrdinalIgnoreCase) != -1)
      {
        string token = AuthenticationHeaderValue.Parse(context.Request.Headers["Authorization"])?.Parameter;
        bool isValid = LimitManager(token);
        if (isValid == false)
        {
          context.Response.StatusCode = (int)HttpStatusCode.TooManyRequests;
          return;
        }
      }
      await _next(context);
    }
    private bool LimitManager(string token)
    {
      CacheModel cacheValue = _memoryCache.Get<CacheModel>(token);
      int expire = Convert.ToInt32(_configuration["RateLimitSettings:Exp"]);//5
      if (cacheValue == null)
      {
        cacheValue = new CacheModel()
        {
          RateCount = 1
        };
        _memoryCache.Set(token, cacheValue, DateTimeOffset.UtcNow.AddMinutes(expire));
        return true;
      }

      cacheValue.RateCount++;
      _memoryCache.Set(token, cacheValue, DateTimeOffset.UtcNow.AddMinutes(expire));
      int limitRate = Convert.ToInt32(_configuration["RateLimitSettings:Limit"]);//5
      if (cacheValue.RateCount >limitRate)
      {
        return false;
      }

      return true;
    }
  }
  public class CacheModel
  {
    public int RateCount { get; set; }
  }
}
