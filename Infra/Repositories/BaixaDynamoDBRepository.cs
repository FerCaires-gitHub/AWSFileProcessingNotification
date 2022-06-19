using System.Collections.Generic;
using System.Threading.Tasks;
using Amazon.DynamoDBv2;
using AWSFileProcessingNotification.Domain.Interfaces.Data;
using AWSFileProcessingNotification.Domain.Interfaces.Repositories;
using AWSFileProcessingNotification.Domain.Models;
using System;
using Amazon.DynamoDBv2.DocumentModel;
using System.Text.Json;

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
        public async Task<Baixa> Get(string id)
        {
            var client  = _factory.GetClient();
            Table table = Table.LoadTable(client,TABLE_NAME);
            var item  = await table.GetItemAsync(id);
            var response  = GetObject<Baixa>(item);
            return response;
        }

        private T GetObject<T>(Document document) where T: class, new()
        {
            var response  = JsonSerializer.Deserialize<T>(document.ToJson());
            return response;
            
        }

        public async Task Insert(Baixa source)
        {
            var client  = _factory.GetClient();
            Table table = Table.LoadTable(client,TABLE_NAME);
            var document = CreateDocument(source);
            await table.PutItemAsync(document);
        }

        private Document CreateDocument(object obj)
        {
            var document = new Document();
            foreach (var item in obj.GetType().GetProperties())
            {
                document[item.Name] = item.GetValue(obj).ToString();
            }
            return document;

        }
    }
}