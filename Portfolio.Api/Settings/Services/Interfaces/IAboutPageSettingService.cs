using Portfolio.Api.Settings.Models.DbModels;
using Portfolio.Api.Settings.Models.Dtos;
using Portfolio.Shared.Dtos;
using System.Threading.Tasks;

namespace Portfolio.Api.Settings.Services.Interfaces
{
  public interface IAboutPageSettingService
  {
    Task<Response<AboutPage>> GetAllByActive(bool? isActive);
    Task<Response<AboutPage>> MultiAddOrUpdate(AboutPageDto aboutItemDto);
  }
}
