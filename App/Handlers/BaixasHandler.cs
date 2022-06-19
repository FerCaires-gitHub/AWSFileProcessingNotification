using System;
using System.IO;
using System.Threading.Tasks;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.SimpleNotificationService;
using AWSFileProcessingNotification.Domain.Interfaces.Hanlders;
using AWSFileProcessingNotification.Domain.Interfaces.Repositories;
using AWSFileProcessingNotification.Domain.Models;
using AWSFileProcessingNotification.Domain.Services;
using Microsoft.Extensions.Logging;

namespace AWSFileProcessingNotification.App.Handlers
{

    public class BaixasHandler : IBaixasHandler
    {
        private readonly IKafkaProducerService<Baixa> _kafkaProducer;
        private readonly IDynamoDBRepository<Baixa> _repository;
        private readonly IAmazonS3 _s3Client;
        private readonly IAmazonSimpleNotificationService _snsClient;
        private readonly ILogger<BaixasHandler> _logger;

        public BaixasHandler(IKafkaProducerService<Baixa> kafkaProducer, 
        IDynamoDBRepository<Baixa> repository, IAmazonS3 s3Client, IAmazonSimpleNotificationService snsClient, ILogger<BaixasHandler> logger)
        {
            _kafkaProducer = kafkaProducer;
            _repository = repository;
            _s3Client = s3Client;
            _snsClient = snsClient;
            _logger = logger;
        }
        public async Task Handle(S3Values message)
        {
            var file = await GetObject(message);
            ProcessaArquivo(file);
        }

        private void ProcessaArquivo(GetObjectResponse file)
        {
            var line  = string.Empty;
            using (var stream = file.ResponseStream)
                {
                    using (var sr = new StreamReader(stream))
                    {
                        while ((line = sr.ReadLine()) != null)
                        {
                            var baixa = Baixa.FromSpanLine(line);
                            _kafkaProducer.SendMessageAsync(baixa);
                            _repository.Insert(baixa);
                        }
                    }

                }
        }

        private async Task<GetObjectResponse> GetObject(S3Values message)
        {
            var file = await _s3Client.GetObjectAsync(new GetObjectRequest{ BucketName = message.BucketName, Key = message.FileName});
            return file;
        }
    }
}