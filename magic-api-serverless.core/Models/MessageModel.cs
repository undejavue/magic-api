namespace magic_api_serverless.core.Models
{
    public record MessageModel
    {
        public int Id { get; set; }
        
        public string UserId { get; set; }
        
        public string Timestamp { get; set; }
        
        public string Text { get; set; }
    }
}