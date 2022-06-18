using System.Collections.Generic;
using System.Threading.Tasks;
using Amazon.DynamoDBv2;
using AWSFileProcessingNotification.Domain.Interfaces.Data;
using AWSFileProcessingNotification.Domain.Interfaces.Repositories;
using AWSFileProcessingNotification.Domain.Models;
using System;
using Amazon.DynamoDBv2.DocumentModel;

namespace AWSFileProcessingNotification.Infra.Repositories
{
    public class BaixaDynamoDBRepository : IDynamoDBRepository<Baixa>
    {
        private const string TABLE_NAME = "Baixas";
        
        private readonly IDataClientFactory<AmazonDynamoDBClient> _factory;

        public BaixaDynamoDBRepository(IDataClientFactory<AmazonDynamoDBClient> factory)
        {
            _factory = factory;
        }
        public Task<Baixa> Get(string id)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<Baixa>> GetAll()
        {
            throw new System.NotImplementedException();
        }

        public Task Insert(Baixa source)
        {
            var client  = _factory.GetClient();
            Table table = Table.LoadTable(client,TABLE_NAME);
            var dictionary  = new Dictionary<string,DynamoDBEntry>();
            var document = new Document();

            return Task.CompletedTask;
        }
    }
}