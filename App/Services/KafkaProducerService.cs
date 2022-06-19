using System.Threading.Tasks;
using AWSFileProcessingNotification.Domain.Services;
using Microsoft.Extensions.Logging;

namespace AWSFileProcessingNotification.App.Services
{
    public class KafkaProducerService<T> : IKafkaProducerService<T>
    {
        public Task SendMessageAsync(T message)
        {
            throw new System.NotImplementedException();
        }
    }
}