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
    
    
    public static Baixa FromSplitLine(string line)
        {
            //  var header = "CPF;CONTRATO;DATA;STATUS;PARCELA;VALOR;DATA_PAGAMENTO";
            var values = line.Split(";");
            var baixa = new Baixa();
            baixa.CPF = values[0];
            baixa.Contrato = Convert.ToInt32(values[1]);
            baixa.Data = Convert.ToDateTime(values[2]);
            baixa.Status = values[3];
            baixa.Parcela = Convert.ToInt32(values[4]);
            baixa.Valor = Convert.ToDecimal(values[5]);
            baixa.Data = Convert.ToDateTime(values[6]);
            return baixa;
        }

        public static Baixa FromSpanLine(string line)
        {
            //  var header = "CPF;CONTRATO;DATA;STATUS;PARCELA;VALOR;DATA_PAGAMENTO";
            var span = line.AsSpan();
            var semicolonPosition = span.IndexOf(";");
            var baixa = new Baixa();
            baixa.CPF = GetValue(ref span,';');
            baixa.Contrato = Convert.ToInt32(GetValue(ref span,';'));
            baixa.Data = Convert.ToDateTime(GetValue(ref span,';'));
            baixa.Status = GetValue(ref span,';');
            baixa.Parcela = Convert.ToInt32(GetValue(ref span,';'));
            baixa.Valor = Convert.ToDecimal(GetValue(ref span,';'));
            baixa.Data = Convert.ToDateTime(GetValue(ref span,';'));
            return baixa;
        }

        private static string GetValue(ref ReadOnlySpan<char> span, char separator)
        {
            var value = string.Empty;
            var index  = span.IndexOf(separator);
            value = span.Slice(0,index).ToString();
            span = span.Slice(index+1);
            return value;
        }
    }

}