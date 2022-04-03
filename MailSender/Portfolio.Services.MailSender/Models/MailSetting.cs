using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace Portfolio.Services.MailSender.Models
{
    public class MailSetting
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
}
