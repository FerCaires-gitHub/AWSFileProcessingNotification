using System.Collections.Generic;
using System.Threading.Tasks;

namespace AWSFileProcessingNotification.Domain.Interfaces.Repositories
{
    public interface IDynamoDBRepository<T>
    {
        Task Insert(T source);
        Task<T> Get(string id);
        Task<IEnumerable<T>> GetAll();
    }
}