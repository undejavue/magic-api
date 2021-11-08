using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace magic_api_serverless.api.Repositories
{
    public interface IDynamoRepository<T>
    {
        Task<T> Get(string id, CancellationToken token);

        Task<IEnumerable<T>> GetList(CancellationToken token);

        Task Create(T request, CancellationToken token);
        
        Task Delete(string id, CancellationToken token);
    }
}