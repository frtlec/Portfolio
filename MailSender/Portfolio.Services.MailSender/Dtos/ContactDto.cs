using Portfolio.Services.MailSender.Models;
using System;

namespace Portfolio.Services.MailSender.Dtos
{
    public class ContactDto
    {
        public string Id { get; set; }
        public string FromMail { get; set; }
        public string Content { get; set; }
        public string Subject { get; set; }
        public bool IsSent { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime SuccessFullSentDate { get; set; }

        public MailSetting MailSetting { get; set; }
    }
}
