using Portfolio.Api.Mail.Dtos;
using Portfolio.Shared.Dtos;
using System.Threading.Tasks;

namespace Portfolio.Api.Mail.Services
{
    public interface IMailSenderService
    {
        Task<Response<NoContent>> Basic(MailSettingDto mailSetting, string subject, string content);
    }
}
