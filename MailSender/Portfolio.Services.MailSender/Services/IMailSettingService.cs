using Portfolio.Services.MailSender.Dtos;
using Portfolio.Shared.Dtos;
using System.Threading.Tasks;

namespace Portfolio.Services.MailSender.Services
{
    public interface IMailSettingService
    {
        Task<Response<MailSettingDto>> GetById(string mailSettingId);
        Task<Response<MailSettingDto>> GetByEmail(string email);
        Task<Response<NoContent>> Create(CreateMailSettingDto createMailSettingDto);
    }
}
