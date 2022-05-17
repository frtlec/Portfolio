namespace Porfolio.Services.Setting.API.Settings
{
  public class DataBaseSettings: IDataBaseSettings
  {
    public string AboutPageCollectionName { get; set; }
    public string LocalizationCollectionName { get; set; }
    public string ConnectionString { get; set; }
    public string DatabaseName { get; set; }
  }
}
