using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using Portfolio.Api.Mail.Infrastructure;
using Portfolio.Api.Mail.Models;
using Portfolio.Api.Settings.Infrastructure;
using Portfolio.Api.Settings.Models.DbModels;
using Portfolio.DataMigration.MongoModels;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Portfolio.DataMigration
{
    // Tek seferlik, tekrar calistirilabilir (idempotent) veri tasima araci:
    // eski maildb/settingdb Mongo koleksiyonlarini okur, yeni tek Postgres'e
    // (mail / settings schema'lari) yazar. Mongo tarafina hicbir yazma yapmaz.
    //
    // Calistirma (sunucuda, eski ve yeni container'lara ayni anda erisimi
    // olan bir ortamdan):
    //   MAIL_MONGO_URL=mongodb://<eski-maildb-host>:27017 \
    //   SETTING_MONGO_URL=mongodb://<eski-settingdb-host>:27018 \
    //   POSTGRES_CONNECTION="User ID=admin;Password=...;Server=<yeni-db-host>;Port=5432;Database=portfoliodb" \
    //   dotnet run --project Portfolio.DataMigration
    public class Program
    {
        public static async Task<int> Main(string[] args)
        {
            string mailMongoUrl = Environment.GetEnvironmentVariable("MAIL_MONGO_URL") ?? "mongodb://localhost:27017";
            string settingMongoUrl = Environment.GetEnvironmentVariable("SETTING_MONGO_URL") ?? "mongodb://localhost:27018";
            string postgresConnection = Environment.GetEnvironmentVariable("POSTGRES_CONNECTION");

            if (string.IsNullOrWhiteSpace(postgresConnection))
            {
                Console.Error.WriteLine("POSTGRES_CONNECTION env degiskeni gerekli.");
                return 1;
            }

            var mailDbOptions = new DbContextOptionsBuilder<MailDbContext>()
                .UseNpgsql(postgresConnection)
                .Options;
            var settingsDbOptions = new DbContextOptionsBuilder<SettingsDbContext>()
                .UseNpgsql(postgresConnection)
                .Options;

            using var mailDbContext = new MailDbContext(mailDbOptions);
            using var settingsDbContext = new SettingsDbContext(settingsDbOptions);

            await MigrateMail(mailMongoUrl, mailDbContext);
            await MigrateSettings(settingMongoUrl, settingsDbContext);

            Console.WriteLine("Tasima tamamlandi.");
            return 0;
        }

        private static async Task MigrateMail(string mongoUrl, MailDbContext target)
        {
            var mongoDb = new MongoClient(mongoUrl).GetDatabase("maildb");

            var mailSettings = await mongoDb.GetCollection<MongoMailSetting>("MailSetting").Find(FilterDefinition<MongoMailSetting>.Empty).ToListAsync();
            int mailSettingsAdded = 0;
            foreach (var src in mailSettings)
            {
                if (await target.MailSettings.AnyAsync(x => x.Id == src.Id)) continue;
                target.MailSettings.Add(new MailSetting
                {
                    Id = src.Id,
                    Mail = src.Mail,
                    ToMail = src.ToMail,
                    CC = src.CC,
                    Password = src.Password,
                    SmtpHost = src.SmtpHost,
                    SmtpPort = src.SmtpPort,
                    EnableSsl = src.EnableSsl,
                });
                mailSettingsAdded++;
            }

            var contacts = await mongoDb.GetCollection<MongoContact>("Contacts").Find(FilterDefinition<MongoContact>.Empty).ToListAsync();
            int contactsAdded = 0;
            foreach (var src in contacts)
            {
                if (await target.Contacts.AnyAsync(x => x.Id == src.Id)) continue;
                target.Contacts.Add(new Contact
                {
                    Id = src.Id,
                    FromMail = src.FromMail,
                    Content = src.Content,
                    CategoryId = src.CategoryId,
                    CategoryName = src.CategoryName,
                    Subject = src.Subject,
                    IsSent = src.IsSent,
                    CreatedDate = src.CreatedDate,
                    SuccessFullSentDate = src.SuccessFullSentDate,
                });
                contactsAdded++;
            }

            await target.SaveChangesAsync();
            Console.WriteLine($"Mail: {mailSettingsAdded} mail setting, {contactsAdded} contact eklendi.");
        }

        private static async Task MigrateSettings(string mongoUrl, SettingsDbContext target)
        {
            var mongoDb = new MongoClient(mongoUrl).GetDatabase("settingdb");

            var aboutPages = await mongoDb.GetCollection<MongoAboutPage>("AboutPage").Find(FilterDefinition<MongoAboutPage>.Empty).ToListAsync();
            int aboutAdded = 0;
            foreach (var src in aboutPages)
            {
                if (await target.AboutPages.AnyAsync(x => x.Id == src.Id)) continue;
                target.AboutPages.Add(new AboutPage
                {
                    Id = src.Id,
                    PortreFileName = src.PortreFileName,
                    Slogan = src.Slogan,
                    Summary = src.Summary,
                    CreatedUserId = src.CreatedUserId,
                    Active = src.Active,
                    UpdatedUserId = src.UpdatedUserId,
                    CreatedDate = src.CreatedDate,
                    UpdatedDate = src.UpdatedDate,
                    Softwares = src.Softwares.Select(s => new AboutSoftware { RowId = s.RowId, Active = s.Active, SvgPath = s.SvgPath, SoftwareName = s.SoftwareName }).ToList(),
                    Businesses = src.Businesses.Select(s => new AboutBusiness { RowId = s.RowId, Active = s.Active, Head = s.Head, Value = s.Value, Foot = s.Foot }).ToList(),
                    Educations = src.Educations.Select(s => new AboutEducation { RowId = s.RowId, Active = s.Active, Head = s.Head, Value = s.Value, Foot = s.Foot }).ToList(),
                    Certifacates = src.Certifacates.Select(s => new AboutCertifacate { RowId = s.RowId, Active = s.Active, Head = s.Head, Value = s.Value }).ToList(),
                    Projects = src.Projects.Select(s => new AboutProject { RowId = s.RowId, Active = s.Active, Head = s.Head, Value = s.Value, Link = s.Link }).ToList(),
                });
                aboutAdded++;
            }

            var localizations = await mongoDb.GetCollection<MongoLocalization>("Localization").Find(FilterDefinition<MongoLocalization>.Empty).ToListAsync();
            int localizationsAdded = 0;
            foreach (var src in localizations)
            {
                if (await target.Localizations.AnyAsync(x => x.Id == src.Id)) continue;
                target.Localizations.Add(new Localization
                {
                    Id = src.Id,
                    CreatedUserId = src.CreatedUserId,
                    UpdatedUserId = src.UpdatedUserId,
                    CreatedDate = src.CreatedDate,
                    UpdatedDate = src.UpdatedDate,
                    Key = src.Key,
                    Value = src.Value,
                    LocalizationType = (LocalizationType)src.LocalizationType,
                });
                localizationsAdded++;
            }

            await target.SaveChangesAsync();
            Console.WriteLine($"Settings: {aboutAdded} about page, {localizationsAdded} localization eklendi.");
        }
    }
}
