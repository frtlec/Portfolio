using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace Portfolio.Services.MailSender.Middlewares
{
  public class AntiForgeryTokenMiddleware
  {
    private readonly RequestDelegate _next;
    private readonly IAntiforgery _antiforgery;

    public AntiForgeryTokenMiddleware(RequestDelegate next, IAntiforgery antiforgery)
    {
      _next = next;
      _antiforgery = antiforgery;
    }

    public Task Invoke(HttpContext context)
    {
      if (context.Request.Path.Value.IndexOf("/api/contacts/create", StringComparison.OrdinalIgnoreCase) != -1)
      {
        var tokens = _antiforgery.GetAndStoreTokens(context);
        context.Response.Cookies.Append("XSRF-TOKEN", tokens.RequestToken, new CookieOptions { HttpOnly = false, Secure = false });
      }
      return _next(context);
    }
  }
}
