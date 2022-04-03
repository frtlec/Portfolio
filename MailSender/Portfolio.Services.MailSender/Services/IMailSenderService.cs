using Portfolio.Services.MailSender.Dtos;
using Portfolio.Shared.Dtos;
using System.Threading.Tasks;

namespace Portfolio.Services.MailSender.Services
{
    public interface IMailSenderService
    {
        Task<Response<NoContent>> Basic(MailSettingDto mailSetting, string subject, string content);
    }
}
