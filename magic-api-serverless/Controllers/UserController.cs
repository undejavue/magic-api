using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using magic_api_serverless.Attributes;
using magic_api_serverless.core.Common;
using magic_api_serverless.core.Models;
using Microsoft.AspNetCore.Mvc;

namespace magic_api_serverless.Controllers
{
    [Route("api/[controller]")]
    [ApiKey]
    public class UserController : ControllerBase
    {
        private readonly IHandler<IEnumerable<UserModel>> getUserListHandler;
        private readonly IHandler<string, UserModel> getUserHandler;
        private readonly IHandler<UserModel, string> createUserHandler;
        private readonly IHandler<string, bool> deleteuserHandler;

        public UserController(
            IHandler<IEnumerable<UserModel>> getUserListHandler, 
            IHandler<string, UserModel> getUserHandler, 
            IHandler<UserModel, string> createUserHandler, 
            IHandler<string, bool> deleteuserHandler)
        {
            this.getUserListHandler = getUserListHandler;
            this.getUserHandler = getUserHandler;
            this.createUserHandler = createUserHandler;
            this.deleteuserHandler = deleteuserHandler;
        }
        
        // GET api/values
        [HttpGet, Route("{userId}")]
        public async Task<UserModel> GetAsync(string userId, CancellationToken token)
        {
            return await getUserHandler.HandleAsync(userId, token);
        }
        
        [HttpGet, Route("all")]
        public async Task<IEnumerable<UserModel>> GetListAsync(CancellationToken token)
        {
            return await getUserListHandler.HandleAsync(token);
        }

        [HttpPost]
        public async Task<string> CreateAsync([FromBody] UserModel request, CancellationToken token)
        {
            return await createUserHandler.HandleAsync(request, token);
        }

        [HttpDelete, Route("{userId}")]
        public async Task<bool> DeleteAsync(string userId, CancellationToken token)
        {
            return await deleteuserHandler.HandleAsync(userId, token);
        }
    }
}