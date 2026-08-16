using System;

namespace Portfolio.Api.Mail.Dtos
{
    public class AddContactDto
    {
        public string FromMail { get; set; }
        public string Subject { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string Content { get; set; }
    }
}
