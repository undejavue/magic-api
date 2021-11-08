using Amazon.DynamoDBv2.DataModel;
using magic_api_serverless.core.Enums;

namespace magic_api_serverless.core.Models
{
    [DynamoDBTable("magic-users")]
    public record UserModel
    {
        [DynamoDBHashKey]
        public string Id { get; set; }

        [DynamoDBProperty]
        public string FirstName { get; set; }

        [DynamoDBProperty]
        public string LastName { get; set; }

        [DynamoDBProperty]
        public  EUserRole Role { get; set; }
        
        [DynamoDBProperty]
        public EUserStatus Status { get; set; }
        
        [DynamoDBProperty]
        public string ImageUrl { get; set; }
        
        [DynamoDBProperty]
        public string About { get; set; }
    }
}