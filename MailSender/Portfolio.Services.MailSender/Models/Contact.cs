using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Portfolio.Services.MailSender.Models
{
    public class Contact
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
        [BsonRepresentation(BsonType.DateTime)]
        public DateTime CreatedDate { get; set; }
        [BsonRepresentation(BsonType.DateTime)]
        public DateTime SuccessFullSentDate { get; set; }

    }
}
