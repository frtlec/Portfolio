using System.Collections.Generic;

namespace Portfolio.Services.MailSender.Dtos
{
    public class MailSettingDto
    {
        public string Id { get; set; }
        public string Mail { get; set; }
        public string Password { get; set; }
        public string SmtpHost { get; set; }
        public List<string> ToMail { get; set; }
        public List<string> CC { get; set; }
        public string SmtpPort { get; set; }
        public bool EnableSsl { get; set; }
    }
}

