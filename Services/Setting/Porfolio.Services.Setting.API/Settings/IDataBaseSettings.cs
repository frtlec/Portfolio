namespace Porfolio.Services.Setting.API.Settings
{
  public interface IDataBaseSettings
  {
    public string AboutPageCollectionName { get; set; }
    public string ConnectionString { get; set; }
    public string DatabaseName { get; set; }
  }
}
