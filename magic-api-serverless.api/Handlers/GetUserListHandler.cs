using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using magic_api_serverless.api.Repositories;
using magic_api_serverless.core.Common;
using magic_api_serverless.core.Models;

namespace magic_api_serverless.api.Handlers
{
    public class GetUserListHandler : IHandler<IEnumerable<UserModel>>
    {
        private readonly IDynamoRepository<UserModel> repository;

        public GetUserListHandler(IDynamoRepository<UserModel> repository)
        {
            this.repository = repository;
        }

        public async Task<IEnumerable<UserModel>> HandleAsync(CancellationToken token)
        {
            return await repository.GetList(token);
        }
    }
}