using Amazon.DynamoDBv2;
using AWSFileProcessingNotification.Domain.Interfaces.Data;

namespace AWSFileProcessingNotification.Infra.Data
{
    public class DynamoDBClientFactory : IDataClientFactory<AmazonDynamoDBClient>
    {
        private readonly IDataConnectionSettingsFactory<AmazonDynamoDBConfig> _config;

        public DynamoDBClientFactory(IDataConnectionSettingsFactory<AmazonDynamoDBConfig> config)
        {
            _config = config;
        }
        public AmazonDynamoDBClient GetClient()
        {
            var client = new AmazonDynamoDBClient(_config.GetSettings());
            return client;
        }
    }
}