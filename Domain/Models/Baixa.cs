using System;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2.Model;

namespace AWSFileProcessingNotification.Domain.Models
{
    public class Baixa 
    {
        public string CPF { get; set; }
        public int Contrato { get; set; }
        public DateTime Data { get; set; }
        public string Status { get; set; }
        public int Parcela { get; set; }
        public decimal Valor { get; set; }
        public DateTime DataPagamento { get; set; }
    }

}