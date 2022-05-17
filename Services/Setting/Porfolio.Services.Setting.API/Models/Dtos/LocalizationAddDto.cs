using Porfolio.Services.Setting.API.Models.DbModels;

namespace Porfolio.Services.Setting.API.Models.Dtos
{
  public class LocalizationAddDto
  {

    public string Key { get; set; }
    public string Value { get; set; }
    public LocalizationType LocalizationType { get; set; }
  }
  public class LocalizationUpdateDto
  {
    public string Id { get; set; }
    public string Key { get; set; }
    public string Value { get; set; }
    public LocalizationType LocalizationType { get; set; }
  }
  public class LocalizationGetByCultureDto
  {
    public string Key { get; set; }
    public LocalizationType LocalizationType { get; set; }
  }
  public class LocalizationGetByCultureDtoResponse
  {
    public string Value { get; set; }
    public string Message { get; set; } = "Bulunamadı";
    public LocalizationType LocalizationType { get; set; }
  }
}
