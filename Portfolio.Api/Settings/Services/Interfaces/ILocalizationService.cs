using Portfolio.Api.Settings.Models.DbModels;
using Portfolio.Api.Settings.Models.Dtos;
using Portfolio.Shared.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Portfolio.Api.Settings.Services.Interfaces
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
