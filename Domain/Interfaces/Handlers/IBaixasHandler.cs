using System.Threading.Tasks;
using AWSFileProcessingNotification.Domain.Models;

namespace AWSFileProcessingNotification.Domain.Interfaces.Hanlders
{
    public interface IBaixasHandler
    {
        Task Handle(S3Values message);
    }
}