namespace AWSFileProcessingNotification.Domain.Interfaces.Data
{
    public interface IDataConnectionSettingsFactory<T>
    {
        T GetSettings();
    }
    
}