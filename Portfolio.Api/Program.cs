using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi;
using Portfolio.Api.Identity;
using Portfolio.Api.Mail.Consumers;
using Portfolio.Api.Mail.Infrastructure;
using Portfolio.Api.Mail.Middlewares;
using Portfolio.Api.Mail.Services;
using Portfolio.Api.Settings.Infrastructure;
using Portfolio.Services.WorkItems.Infrastructure;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;

JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Remove("sub");

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowOrigin", policy =>
    {
        policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
});

builder.Services.AddSingleton<TokenService>();

builder.Services.AddAuthorization(opts =>
{
    opts.AddPolicy("ReadAndWrite", policy =>
    {
        policy.RequireClaim("scope", "selin.ozoglu.com.work.write", "selin.ozoglu.com.work.read");
    });
    opts.AddPolicy("WriteEditWork", policy =>
    {
        policy.RequireClaim("scope", "selin.ozoglu.com.work.write");
    });
});

builder.Services.AddControllers();

// Tek Postgres instance, modul basina ayri schema.
string connectionString = builder.Configuration.GetConnectionString("PostgreSql");

builder.Services.AddDbContext<WorkItemsDbContext>(opt =>
    opt.UseNpgsql(connectionString, sql => sql.MigrationsAssembly("Portfolio.Services.WorkItems.Infrastructure")));

builder.Services.AddDbContext<IdentityDataContext>(opt => opt.UseNpgsql(connectionString));

builder.Services.AddDbContext<MailDbContext>(opt => opt.UseNpgsql(connectionString));

builder.Services.AddDbContext<SettingsDbContext>(opt => opt.UseNpgsql(connectionString));

// AddIdentity kendi icinde AddAuthentication cagirip cookie semasini
// varsayilan yapar; JWT bearer'in gercek varsayilan sema olarak kalmasi
// icin AddAuthentication().AddJwtBearer() BURADAN SONRA, en son cagriliyor.
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<IdentityDataContext>()
    .AddDefaultTokenProviders();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(opt =>
{
    var tokenService = new TokenService(builder.Configuration);
    opt.TokenValidationParameters = tokenService.GetValidationParameters();
});

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Portfolio.Services.WorkItems.Application.Handlers.GetAllWorkByFilterHandler).Assembly));

builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddMaps(typeof(Portfolio.Api.Mail.Mapping.GeneralMapping).Assembly);
    cfg.AddMaps(typeof(Portfolio.Api.Settings.Models.GeneralMapping).Assembly);
});

builder.Services.AddMemoryCache();

builder.Services.AddTransient<Portfolio.Api.Settings.Services.Interfaces.IAboutPageSettingService, Portfolio.Api.Settings.Services.AboutPageSettingService>();
builder.Services.AddTransient<Portfolio.Api.Settings.Services.Interfaces.ILocalizationService, Portfolio.Api.Settings.Services.LocalizationService>();
builder.Services.AddTransient<IMailSettingService, MailSettingService>();
builder.Services.AddTransient<IContactService, ContactService>();
builder.Services.AddTransient<IMailSenderService, MailSenderService>();

builder.Services.AddAntiforgery(options => options.HeaderName = "X-XSRF-TOKEN");

builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<ContactMailSenderCommandConsumer>();
    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host(builder.Configuration["RabbitMQUrl"], "/", host =>
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

builder.Services.AddSwaggerGen(c =>
{
    c.EnableAnnotations();
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Portfolio.Api", Version = "v1" });
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    services.GetRequiredService<WorkItemsDbContext>().Database.Migrate();
    services.GetRequiredService<IdentityDataContext>().Database.Migrate();
    services.GetRequiredService<MailDbContext>().Database.Migrate();
    services.GetRequiredService<SettingsDbContext>().Database.Migrate();

    await SeedAdminUsers(services);
}

if (app.Environment.IsDevelopment())
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

app.MapControllers();

app.Run();

static async Task SeedAdminUsers(IServiceProvider services)
{
    var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

    if (!await roleManager.RoleExistsAsync("admin"))
    {
        await roleManager.CreateAsync(new IdentityRole("admin"));
    }

    await EnsureAdminUser(userManager, "zaferkrk", "zafer.krk@hotmail.com", "İstanbul", "ZK.147olu");
    await EnsureAdminUser(userManager, "selino", "selin.ozoglu98@gmail.com", "İstanbul", "SL!x123");
}

static async Task EnsureAdminUser(UserManager<ApplicationUser> userManager, string userName, string email, string city, string password)
{
    var existing = await userManager.FindByEmailAsync(email);
    if (existing != null)
    {
        return;
    }

    var user = new ApplicationUser { UserName = userName, Email = email, City = city };
    var result = await userManager.CreateAsync(user, password);
    if (result.Succeeded)
    {
        await userManager.AddToRoleAsync(user, "admin");
    }
}
