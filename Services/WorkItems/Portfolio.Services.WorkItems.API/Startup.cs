using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Portfolio.Services.WorkItems.Infrastructure;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Threading.Tasks;

namespace Portfolio.Services.WorkItems.API
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
      JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Remove("sub");
      services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).
                  AddJwtBearer(opt =>
                  {
                    opt.Authority = Configuration["IdentityServerURL"];
                    opt.Audience = "resource_workitem";
                    opt.RequireHttpsMetadata = true;
                    //opt.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                    //{
                    //  ValidateIssuer = false,
                    //  ValidateAudience = false,
                      
                    //};
                    opt.Events = new JwtBearerEvents
                    {
                      OnAuthenticationFailed = context =>
                      {
                        var x = new
                        {
                          LogName="WorkItem",
                          ExMessage = context.Exception.Message,
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

      services.AddControllers();

      services.AddDbContext<WorkItemsDbContext>(opt =>
      {

        opt.UseNpgsql(Configuration.GetConnectionString("PostgreSql"), configure =>
        {
          configure.MigrationsAssembly("Portfolio.Services.WorkItems.Infrastructure");
        });
      });
      services.AddMediatR(typeof(Portfolio.Services.WorkItems.Application.Handlers.GetAllWorkByFilterHandler).Assembly);

      services.AddSwaggerGen(c =>
      {
        c.EnableAnnotations();
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "Portfolio.Services.WorkItems.API", Version = "v1" });
      });

    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
        app.UseSwagger();
        app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Portfolio.Services.WorkItems.API v1"));
      }

      app.UseCors("AllowOrigin");
      app.UseHttpsRedirection();
      app.UseHsts();
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
