using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

// Eski Mongo dokumanlarinin okunmasi icin sadece bu projeye ozel POCO'lar.
// Portfolio.Services.MailSender / Setting.API projelerindeki modeller artik
// EF/Postgres hedefli oldugu icin (BsonId yok), onlari Mongo okumasi icin
// tekrar kullanmiyoruz.
namespace Portfolio.DataMigration.MongoModels
{
    public class MongoContact
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string FromMail { get; set; }
        public string Content { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string Subject { get; set; }
        public bool IsSent { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime SuccessFullSentDate { get; set; }
    }

    public class MongoMailSetting
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Mail { get; set; }
        public List<string> ToMail { get; set; }
        public List<string> CC { get; set; }
        public string Password { get; set; }
        public string SmtpHost { get; set; }
        public string SmtpPort { get; set; }
        public bool EnableSsl { get; set; }
    }

    public class MongoAboutPage
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string PortreFileName { get; set; }
        public string Slogan { get; set; }
        public string Summary { get; set; }
        public int CreatedUserId { get; set; }
        public bool? Active { get; set; }
        public int UpdatedUserId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public List<MongoAboutSoftware> Softwares { get; set; } = new();
        public List<MongoAboutBusiness> Businesses { get; set; } = new();
        public List<MongoAboutEducation> Educations { get; set; } = new();
        public List<MongoAboutCertifacate> Certifacates { get; set; } = new();
        public List<MongoAboutProject> Projects { get; set; } = new();
    }

    public class MongoAboutAggregate
    {
        public Guid RowId { get; set; }
        public bool Active { get; set; }
    }

    public class MongoAboutSoftware : MongoAboutAggregate
    {
        public string SvgPath { get; set; }
        public string SoftwareName { get; set; }
    }

    public class MongoAboutBusiness : MongoAboutAggregate
    {
        public string Head { get; set; }
        public string Value { get; set; }
        public string Foot { get; set; }
    }

    public class MongoAboutEducation : MongoAboutAggregate
    {
        public string Head { get; set; }
        public string Value { get; set; }
        public string Foot { get; set; }
    }

    public class MongoAboutCertifacate : MongoAboutAggregate
    {
        public string Head { get; set; }
        public string Value { get; set; }
    }

    public class MongoAboutProject : MongoAboutAggregate
    {
        public string Head { get; set; }
        public string Value { get; set; }
        public string Link { get; set; }
    }

    public class MongoLocalization
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public int CreatedUserId { get; set; }
        public int UpdatedUserId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
        public int LocalizationType { get; set; }
    }
}
