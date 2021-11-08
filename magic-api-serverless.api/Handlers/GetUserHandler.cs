using System.Threading;
using System.Threading.Tasks;
using magic_api_serverless.api.Repositories;
using magic_api_serverless.core.Common;
using magic_api_serverless.core.Models;

namespace magic_api_serverless.api.Handlers
{
    public class GetUserHandler : IHandler<string, UserModel>
    {
        private readonly IDynamoRepository<UserModel> repository;

        public GetUserHandler(IDynamoRepository<UserModel> repository)
        {
            this.repository = repository;
        }

        public async Task<UserModel> HandleAsync(string userId, CancellationToken token)
        {
            return await repository.Get(userId, token);
        }
    }
}