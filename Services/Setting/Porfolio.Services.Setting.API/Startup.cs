using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Porfolio.Services.Setting.API.Services;
using Porfolio.Services.Setting.API.Services.Interfaces;
using Porfolio.Services.Setting.API.Settings;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace Porfolio.Services.Setting.API
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
      JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Remove("sub");
      services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).
          AddJwtBearer(opt =>
          {
            opt.Authority = Configuration["IdentityServerURL"];
            opt.Audience = "resource_settings";
            opt.RequireHttpsMetadata = false;
          });
      services.AddAuthorization(opts =>
      {
        opts.AddPolicy("ReadAndWrite", policy =>
        {
          policy.RequireClaim("scope", new[] { "selin.ozoglu.com.work.write", "selin.ozoglu.com.work.read" });
        });
        opts.AddPolicy("WriteEditWork", policy =>
        {
          policy.RequireClaim("scope", new[] { "selin.ozoglu.com.work.write" });
        });
      });
      services.AddControllers();
      services.AddSwaggerGen(c =>
      {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "Porfolio.Services.Setting.API", Version = "v1" });
      });
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
      services.AddAutoMapper(typeof(Startup));
      services.AddTransient<IAboutPageSettingService, AboutPageSettingService>();
      services.AddTransient<ILocalizationService, LocalizationService>();
      services.Configure<DataBaseSettings>(Configuration.GetSection("DatabaseSettings"));
      services.AddSingleton<IDataBaseSettings>(sp =>
      {
        return sp.GetRequiredService<IOptions<DataBaseSettings>>().Value;
      });
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
        app.UseSwagger();
        app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Porfolio.Services.Setting.API v1"));
      }
      app.UseCors("AllowOrigin");
      //app.UseHttpsRedirection();
      //app.UseHsts();
      app.UseRouting();

      app.UseAuthentication();
      app.UseAuthorization();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
      });
    }
  }
}
