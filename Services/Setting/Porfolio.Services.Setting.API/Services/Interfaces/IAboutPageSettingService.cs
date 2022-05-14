using Porfolio.Services.Setting.API.Models.DbModels;
using Porfolio.Services.Setting.API.Models.Dtos;
using Portfolio.Shared.Dtos;
using System.Threading.Tasks;

namespace Porfolio.Services.Setting.API.Services.Interfaces
{
  public interface IAboutPageSettingService
  {
    Task<Response<AboutPage>> GetAllByActive(bool? isActive);
    Task<Response<AboutPage>> MultiAddOrUpdate(AboutPageDto aboutItemDto);
  }
}
