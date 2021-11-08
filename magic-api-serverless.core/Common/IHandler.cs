using System.Threading;
using System.Threading.Tasks;

namespace magic_api_serverless.core.Common
{
    public interface IHandler <in TRequest, TResult>
    {
        Task<TResult> HandleAsync(TRequest request, CancellationToken token);
    }
    
    public interface IHandler <TResult>
    {
        Task<TResult> HandleAsync(CancellationToken token);
    }
}