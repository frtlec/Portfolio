using Porfolio.Services.Setting.API.Models.DbModels;
using Porfolio.Services.Setting.API.Models.Dtos;
using Portfolio.Shared.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Porfolio.Services.Setting.API.Services.Interfaces
{
  public interface ILocalizationService
  {
    public Task<Response<List<Localization>>> GetAll();
    Task<Response<LocalizationGetByCultureDtoResponse>> GetByCulture(LocalizationGetByCultureDto getByCultureDto);
    public Task<Response<Localization>> Add(LocalizationAddDto localization);
    public Task<Response<Localization>> Update(LocalizationUpdateDto localization);
    public Task<Response<NoContent>> Delete(string id);
    
  }
}
