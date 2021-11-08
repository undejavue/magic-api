using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;

using magic_api_serverless.core.Models;

namespace magic_api_serverless.api.Repositories
{
    public class UserRepository : IDynamoRepository<UserModel>
    {

        private readonly IDynamoDBContext context;

        public UserRepository(IAmazonDynamoDB amazonDynamoDb)
        {
            this.context = new DynamoDBContext(amazonDynamoDb);
        }

        public async Task<UserModel> Get(string id, CancellationToken token)
        {
            return await context.LoadAsync<UserModel>(id, new DynamoDBContextConfig
                {
                    ConsistentRead = true,
                },
                token);
        }

        public async Task<IEnumerable<UserModel>> GetList(CancellationToken token)
        {
            return await context.ScanAsync<UserModel>(new List<ScanCondition>()).GetRemainingAsync(token);
        }

        public async Task Create(UserModel request, CancellationToken token)
        {
            await context.SaveAsync(request, token);
        }

        public async Task Delete(string id, CancellationToken token)
        {
            await context.DeleteAsync(id, token);
        }
    }
}