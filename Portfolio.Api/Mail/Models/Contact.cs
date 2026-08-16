using System;

namespace Portfolio.Api.Mail.Models
{
    public class Contact
    {
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
}
