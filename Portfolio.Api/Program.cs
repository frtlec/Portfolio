using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Portfolio.Api.Identity;
using Portfolio.Api.Mail.Infrastructure;
using Portfolio.Api.Settings.Infrastructure;
using Portfolio.Services.WorkItems.Infrastructure;
using System;

namespace Portfolio.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                services.GetRequiredService<WorkItemsDbContext>().Database.Migrate();
                services.GetRequiredService<IdentityDataContext>().Database.Migrate();
                services.GetRequiredService<MailDbContext>().Database.Migrate();
                services.GetRequiredService<SettingsDbContext>().Database.Migrate();

                SeedAdminUsers(services).GetAwaiter().GetResult();
            }

            host.Run();
        }

        private static async System.Threading.Tasks.Task SeedAdminUsers(IServiceProvider services)
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

        private static async System.Threading.Tasks.Task EnsureAdminUser(UserManager<ApplicationUser> userManager, string userName, string email, string city, string password)
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

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
