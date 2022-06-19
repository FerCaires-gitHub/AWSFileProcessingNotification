namespace AWSFileProcessingNotification.Domain.Models
{
    public class Config
    {
        public string DynamoURL { get; set; }
        public int MaxQtdMessages { get; set; }
    }
}