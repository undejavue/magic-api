using System.Threading;
using System.Threading.Tasks;
using magic_api_serverless.api.Repositories;
using magic_api_serverless.core.Common;
using magic_api_serverless.core.Models;

namespace magic_api_serverless.api.Handlers
{
    public class DeleteUserHandler : IHandler<string, bool>
    {
        private readonly IDynamoRepository<UserModel> repository;

        public DeleteUserHandler(IDynamoRepository<UserModel> repository)
        {
            this.repository = repository;
        }

        public async Task<bool> HandleAsync(string userId, CancellationToken token)
        {
            await repository.Delete(userId, token);

            return true;
        }
    }
}