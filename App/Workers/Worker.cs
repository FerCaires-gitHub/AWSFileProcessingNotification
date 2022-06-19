using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Amazon.Lambda.S3Events;
using Amazon.SQS;
using Amazon.SQS.Model;
using AWSFileProcessingNotification.Domain.Interfaces.Hanlders;
using AWSFileProcessingNotification.Domain.Models;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace AWSFileProcessingNotification.App.Workers
{
    public class Worker : BackgroundService
    {
        private const string QUEUE_NAME = "";
        private readonly ILogger<Worker> _logger;
        private readonly IBaixasHandler _handler;
        private readonly IAmazonSQS _sqsClient;
        private readonly IOptions<Config> _options;

        public Worker(ILogger<Worker> logger, IBaixasHandler handler, IAmazonSQS sqsClient, IOptions<Config> options)
        {
            _logger = logger;
            _handler = handler;
            _sqsClient = sqsClient;
            _options = options;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {

            _logger.LogInformation($"Recuperando a URL da fila:{QUEUE_NAME}");
            var queueURL = await _sqsClient.GetQueueUrlAsync(QUEUE_NAME);
            while (!stoppingToken.IsCancellationRequested)
            {
                var messages = await  _sqsClient.ReceiveMessageAsync(new ReceiveMessageRequest{MaxNumberOfMessages =_options.Value.MaxQtdMessages, QueueUrl = queueURL.QueueUrl});
                _logger.LogInformation($"Encontradas:{messages.Messages.Count()} mensagens para processamento");
                foreach (var message in messages.Messages)
                {
                    var s3Message = GetS3Values(message);
                    await _handler.Handle(s3Message);
                    await _sqsClient.DeleteMessageAsync(new DeleteMessageRequest{QueueUrl = queueURL.QueueUrl, ReceiptHandle = message.ReceiptHandle});
                }
                await Task.Delay(1000, stoppingToken);
            }
        }

        private S3Values GetS3Values(Message message)
        {
            var s3Event = JsonSerializer.Deserialize<S3Event>(message.Body);
            var eventData = s3Event.Records[0].S3;
            var bucketName = eventData.Bucket.Name;
            var key = eventData.Object.Key;
            return new S3Values{ BucketName = bucketName, FileName = key};
        }
    }
}
