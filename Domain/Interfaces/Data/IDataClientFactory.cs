namespace AWSFileProcessingNotification.Domain.Interfaces.Data
{
    public interface IDataClientFactory<T>
    {
        T GetClient();
    }
}