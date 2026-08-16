using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Portfolio.Api.Identity;
using Portfolio.Api.Mail.Consumers;
using Portfolio.Api.Mail.Infrastructure;
using Portfolio.Api.Mail.Middlewares;
using Portfolio.Api.Mail.Services;
using Portfolio.Api.Settings.Infrastructure;
using Portfolio.Services.WorkItems.Infrastructure;
using System;
using System.IdentityModel.Tokens.Jwt;

namespace Portfolio.Api
{
  public class Startup
  {
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
      JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Remove("sub");

      services.AddCors(options =>
      {
        options.AddPolicy("AllowOrigin", builder =>
        {
          builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
        });
      });

      services.AddSingleton<TokenService>();

      services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(opt =>
        {
          var tokenService = new TokenService(Configuration);
          opt.TokenValidationParameters = tokenService.GetValidationParameters();
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

      // Tek Postgres instance, modul basina ayri schema.
      string connectionString = Configuration.GetConnectionString("PostgreSql");

      services.AddDbContext<WorkItemsDbContext>(opt =>
        opt.UseNpgsql(connectionString, sql => sql.MigrationsAssembly("Portfolio.Services.WorkItems.Infrastructure")));

      services.AddDbContext<IdentityDataContext>(opt => opt.UseNpgsql(connectionString));

      services.AddDbContext<MailDbContext>(opt => opt.UseNpgsql(connectionString));

      services.AddDbContext<SettingsDbContext>(opt => opt.UseNpgsql(connectionString));

      services.AddIdentity<ApplicationUser, IdentityRole>()
        .AddEntityFrameworkStores<IdentityDataContext>()
        .AddDefaultTokenProviders();

      services.AddMediatR(typeof(Portfolio.Services.WorkItems.Application.Handlers.GetAllWorkByFilterHandler).Assembly);

      services.AddAutoMapper(
        typeof(Portfolio.Api.Mail.Mapping.GeneralMapping),
        typeof(Portfolio.Api.Settings.Models.GeneralMapping));

      services.AddMemoryCache();

      services.AddTransient<Portfolio.Api.Settings.Services.Interfaces.IAboutPageSettingService, Portfolio.Api.Settings.Services.AboutPageSettingService>();
      services.AddTransient<Portfolio.Api.Settings.Services.Interfaces.ILocalizationService, Portfolio.Api.Settings.Services.LocalizationService>();
      services.AddTransient<IMailSettingService, MailSettingService>();
      services.AddTransient<IContactService, ContactService>();
      services.AddTransient<IMailSenderService, MailSenderService>();

      services.AddAntiforgery(options => options.HeaderName = "X-XSRF-TOKEN");

      services.AddMassTransit(x =>
      {
        x.AddConsumer<ContactMailSenderCommandConsumer>();
        x.UsingRabbitMq((context, cfg) =>
        {
          cfg.Host(Configuration["RabbitMQUrl"], "/", host =>
          {
            host.Username("guest");
            host.Password("guest");
          });
          cfg.ReceiveEndpoint("contact-mail-sender-command-consumer", e =>
          {
            e.ConfigureConsumer<ContactMailSenderCommandConsumer>(context);
          });
        });
      });

      services.AddSwaggerGen(c =>
      {
        c.EnableAnnotations();
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "Portfolio.Api", Version = "v1" });
      });
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
        app.UseSwagger();
        app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Portfolio.Api v1"));
      }

      app.UseCors("AllowOrigin");
      app.UseStaticFiles();

      app.UseRouting();

      app.UseMiddleware<AntiForgeryTokenMiddleware>();
      app.UseMiddleware<CreateMailLimiterMiddleware>();

      app.UseAuthentication();
      app.UseAuthorization();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
      });
    }
  }
}
