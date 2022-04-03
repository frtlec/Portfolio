
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using Portfolio.Services.MailSender.Dtos;
using Portfolio.Shared.Dtos;
using System;
using System.Threading.Tasks;

namespace Portfolio.Services.MailSender.Services
{
    public class MailSenderService: IMailSenderService
    {
        public async Task<Response<NoContent>> Basic(MailSettingDto mailSetting,string subject,string content)
        {
            using var smtp = new SmtpClient();
            try
            {
                if (mailSetting==null)
                {
                    return Response<NoContent>.Fail("mail setting not found",500);
                }
                var email = new MimeMessage();
                email.Sender = MailboxAddress.Parse(mailSetting.Mail);

                foreach (var  toMail in mailSetting.ToMail)
                {
                    email.To.Add(MailboxAddress.Parse(toMail));
                }
                foreach (var cc in mailSetting.CC)
                {
                    email.Cc.Add(MailboxAddress.Parse(cc));
                }

                email.Subject = subject;
                var builder = new BodyBuilder();
                builder.HtmlBody = content;
                email.Body = builder.ToMessageBody();
            
                int smtpPort = Convert.ToInt32(String.IsNullOrEmpty(mailSetting.SmtpPort)?587:mailSetting.SmtpPort);
                SecureSocketOptions secureSocketOptions = mailSetting.EnableSsl ? SecureSocketOptions.StartTls : SecureSocketOptions.None;
                smtp.Connect(mailSetting.SmtpHost, smtpPort, secureSocketOptions);
                smtp.Authenticate(mailSetting.Mail, mailSetting.Password);
                await smtp.SendAsync(email);
                return Response<NoContent>.Success(200);
            }
            catch(Exception ex)
            {
                return Response<NoContent>.Fail(ex.Message,500);
            }
            finally
            {
                await smtp.DisconnectAsync(true);
                smtp.Dispose();
            }
  
        }
    }
}
