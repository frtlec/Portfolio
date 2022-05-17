namespace Porfolio.Services.Setting.API.Models.DbModels
{
  public class Localization:CommonDbModel
  {
    public string Key { get; set; }
    public string Value { get; set; }
    public LocalizationType LocalizationType { get; set; }
  }
  public enum LocalizationType
  {
    
    EN=1,
    FR=2

  }
}
