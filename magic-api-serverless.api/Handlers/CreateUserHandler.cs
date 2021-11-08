using System;
using System.Threading;
using System.Threading.Tasks;
using magic_api_serverless.api.Repositories;
using magic_api_serverless.core.Common;
using magic_api_serverless.core.Models;

namespace magic_api_serverless.api.Handlers
{
    public class CreateUserHandler : IHandler<UserModel, string>
    {
        private readonly IDynamoRepository<UserModel> repository;

        public CreateUserHandler(IDynamoRepository<UserModel> repository)
        {
            this.repository = repository;
        }

        public async Task<string> HandleAsync(UserModel request, CancellationToken token)
        {
            var guid = Guid.NewGuid().ToString();
            var user = request with {Id = guid};
            
            await repository.Create(user, token);

            return guid;
        }
    }
}