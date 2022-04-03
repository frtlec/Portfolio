namespace Portfolio.Services.MailSender.Settings
{
    public class DataBaseSettings: IDataBaseSettings
    {
        public string ContactCollectionName { get; set; }
        public string MailSettingCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
