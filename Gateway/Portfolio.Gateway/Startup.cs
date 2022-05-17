using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Portfolio.Gateway.DelegateHandlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portfolio.Gateway
{
  public class Startup
  {
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddHttpClient<TokenExchangeDelegateHandler>();
      services.AddCors(options =>
      {
        options.AddPolicy(
                  name: "AllowOrigin",
                  builder =>
                  {
                    builder.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader();
                  });
      });
      services.AddAuthentication().AddJwtBearer("GatewayAuthenticationScheme", options => {

        options.Authority = Configuration["IdentityServerURL"];
        options.Audience = "resource_gateway";
        options.RequireHttpsMetadata = false;
        //options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
        //{
        //  ValidateIssuer = false,
        //  ValidateAudience = false,

        //};
        options.Events = new JwtBearerEvents
        {
          OnAuthenticationFailed = context =>
          {
            var x = new
            {
              LogName = "GatewayP",
              exMessage = context.Exception.Message,
              Audience = context.Options.Audience,
              Authority = context.Options.Authority
            };
            Console.WriteLine(System.Text.Json.JsonSerializer.Serialize(x));
            //Log failed authentications
            return Task.CompletedTask;
          },
          OnTokenValidated = context =>
          {
            //Log successful authentications
            return Task.CompletedTask;
          }
        };
      });
      services.AddOcelot().AddDelegatingHandler<TokenExchangeDelegateHandler>();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public async void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
       
      }
      app.UseDeveloperExceptionPage();
      app.UseCors("AllowOrigin");
      app.UseHttpsRedirection();
      app.UseHsts();
      await app.UseOcelot();
    }
  }
}
