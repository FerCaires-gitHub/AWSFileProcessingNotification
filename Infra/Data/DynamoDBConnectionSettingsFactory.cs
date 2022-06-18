using Amazon.DynamoDBv2;
using AWSFileProcessingNotification.Domain.Interfaces.Data;
using AWSFileProcessingNotification.Domain.Models;
using Microsoft.Extensions.Options;

namespace AWSFileProcessingNotification.Infra.Data
{
    public class DynamoDBConnectionSettingsFactory : IDataConnectionSettingsFactory<AmazonDynamoDBConfig>
    {
        private readonly IOptions<Config> _options;

        public DynamoDBConnectionSettingsFactory(IOptions<Config> options)
        {
            _options = options;
        }

        public AmazonDynamoDBConfig GetSettings()
        {
            var config = new AmazonDynamoDBConfig();
            config.ServiceURL = _options.Value.DynamoURL;
            return config;
        }
    }
}