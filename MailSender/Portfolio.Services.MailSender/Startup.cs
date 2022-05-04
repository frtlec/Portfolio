using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Portfolio.Services.MailSender.Consumers;
using Portfolio.Services.MailSender.Middlewares;
using Portfolio.Services.MailSender.Services;
using Portfolio.Services.MailSender.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portfolio.Services.MailSender
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
      services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).
             AddJwtBearer(opt =>
             {
               opt.Authority = Configuration["IdentityServerURL"];
               opt.Audience = "resource_mailsender";
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
      services.AddMassTransit(x =>
      {
        x.AddConsumer<ContactMailSenderCommandConsumer>();
              // Default Port : 5672
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
      services.AddTransient<IMailSettingService, MailSettingService>();
      services.AddTransient<IContactService, ContactService>();
      services.AddTransient<IMailSenderService, MailSenderService>();
      services.AddAutoMapper(typeof(Startup));
      services.AddControllers();
      services.AddMemoryCache();
      services.AddSwaggerGen(c =>
      {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "Portfolio.Services.MailSender", Version = "v1" });
      });
      services.Configure<DataBaseSettings>(Configuration.GetSection("DatabaseSettings"));
      services.AddAntiforgery(options => options.HeaderName = "X-XSRF-TOKEN");
      services.AddSingleton<IDataBaseSettings>(sp =>
      {
        return sp.GetRequiredService<IOptions<DataBaseSettings>>().Value;
      });
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {

      app.UseDeveloperExceptionPage();
      if (env.IsDevelopment())
      {
        app.UseSwagger();
        app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Portfolio.Services.MailSender v1"));
      }

      app.UseCors("AllowOrigin");
      //app.UseHttpsRedirection();
      app.UseMiddleware<AntiForgeryTokenMiddleware>();
      app.UseMiddleware<CreateMailLimiterMiddleware>();
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
