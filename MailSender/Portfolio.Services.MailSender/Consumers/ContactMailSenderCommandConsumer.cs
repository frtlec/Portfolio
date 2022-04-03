using MassTransit;
using Portfolio.Services.MailSender.Dtos;
using Portfolio.Services.MailSender.Models;
using Portfolio.Services.MailSender.Services;
using Portfolio.Shared.RabbitMQ.Messages;
using System;
using System.Threading.Tasks;

namespace Portfolio.Services.MailSender.Consumers
{
    public class ContactMailSenderCommandConsumer : IConsumer<ContactMailSendCommand>
    {
        private readonly IContactService _contactService;
        private readonly IMailSenderService _mailSenderService;
        private readonly IMailSettingService  _mailSettingService;

        public ContactMailSenderCommandConsumer(IContactService contactService, IMailSenderService mailSenderService, IMailSettingService mailSettingService)
        {
            _contactService = contactService;
            _mailSenderService = mailSenderService;
            _mailSettingService = mailSettingService;
        }

        public async Task Consume(ConsumeContext<ContactMailSendCommand> context)
        {
            try
            {
                var getByIdResult = await _contactService.GetById(context.Message.ContactId);

                if (getByIdResult.IsSuccessful == false)
                {
                    throw new Exception(String.Join(",", getByIdResult.Errors));
                }

                ContactDto contact = getByIdResult.Data;

                var getByIdMailSetting = await _mailSettingService.GetByEmail(contact.FromMail);
                if (getByIdMailSetting.IsSuccessful == false)
                {
                    throw new Exception($"{contact.FromMail} Mail Settings notfound!");
                }

                MailSettingDto mailSetting = getByIdMailSetting.Data;
                var basicMailResult = await _mailSenderService.Basic(mailSetting, contact.Subject, contact.Content);
                if (basicMailResult.IsSuccessful == false)
                {
                    throw new Exception($"Mail not sent");
                }

                await _contactService.SuccessSentMailAfterContactUpdate(contact.Id);

            }
            catch (Exception ex)
            {

                throw ex;
            }

        }


      

    }
}
