using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Amazon.S3;
using Amazon.SQS;
using Amazon.SimpleNotificationService;
using Amazon.DynamoDBv2;


namespace AWSFileProcessingNotification.App.IoC
{
    public class DependencyContainer
    {
     public static void RegisterServices(IServiceCollection services)   
     {
        var configuration = new ConfigurationBuilder()
                            .AddJsonFile("appsettings.json",true,true)
                            .AddEnvironmentVariables()
                            .Build();
        var options =configuration.GetAWSOptions("AWS");
        services.AddDefaultAWSOptions(options);
        services.AddAWSService<IAmazonS3>();
        services.AddAWSService<IAmazonSQS>();
        services.AddAWSService<IAmazonSimpleNotificationService>();
        services.AddAWSService<IAmazonDynamoDB>();
     }
    }
}