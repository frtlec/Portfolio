using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using Portfolio.Services.MailSender.Models;
using Portfolio.Services.MailSender.Services;
using Portfolio.Services.MailSender.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portfolio.Services.MailSender
{
  public class Program
  {
    public static void Main(string[] args)
    {
      var host = CreateHostBuilder(args).Build();


      using (var scope = host.Services.CreateScope())
      {
        var serviceProvider = scope.ServiceProvider;
        var applicationDbContext = serviceProvider.GetRequiredService<IDataBaseSettings>();
        var config = new ConfigurationBuilder()
             .AddJsonFile("appsettings.json", optional: false)
             .Build();
        MongoClient mongoClient = new MongoClient(applicationDbContext.ConnectionString);
        IMongoDatabase mongoDatabase = mongoClient.GetDatabase(applicationDbContext.DatabaseName);
        IMongoCollection<MailSetting> mailSettingCollection =  mongoDatabase.GetCollection<MailSetting>(applicationDbContext.MailSettingCollectionName);
        bool isExistsItem = mailSettingCollection.Find(_ => true).Any();
        if (isExistsItem==false)
        {
          mailSettingCollection.InsertOneAsync(new MailSetting 
          {
          SmtpHost = "smtp.gmail.com",
          SmtpPort = "587",
          EnableSsl = true,
           CC=new List<string> { "zafer.krk@hotmail.com" },
          Mail="selinportfolio@gmail.com",
          Password="SK.1905SO",
          ToMail= new List<string> { "selinportfolio@gmail.com","selin.ozoglu98@gmail.com" }

          });
        }


      }
      host.Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
              webBuilder.UseStartup<Startup>();
            });
  }
}
