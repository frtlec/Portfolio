using Portfolio.Shared.WebSiteType;
using System;
using System.Collections.Generic;
using System.Text;

namespace Portfolio.Shared.RabbitMQ.Messages
{
    public class ContactMailSendCommand
    {
        public string ContactId { get; set; }

    }
}
