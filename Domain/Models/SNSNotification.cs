using System;

namespace AWSFileProcessingNotification.Domain.Models
{
    public class SNSNotification
    {
        public Guid Id { get; set; }
        public DateTime Data { get; set; }
        public string JsonMesage { get; set; }
    }
    
}