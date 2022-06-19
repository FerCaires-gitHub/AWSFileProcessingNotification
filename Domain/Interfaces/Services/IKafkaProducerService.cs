using System.Threading.Tasks;

namespace AWSFileProcessingNotification.Domain.Services
{
    public interface IKafkaProducerService<T>
    {
        Task SendMessageAsync(T message);
    }
}