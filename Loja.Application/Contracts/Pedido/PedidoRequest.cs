using Loja.Application.Contract.Produto;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Loja.Application.Contract.Pedido
{
    public class PedidoRequest
    {
        public Guid? Id { get; set; }
        public DateTime Data { get; set; } = DateTime.Now;
        public Guid ClienteId { get; set; }
        public List<ProdutoRequest> Produtos { get; set; } = new List<ProdutoRequest>();
        public decimal Valor { get; set; }
        public decimal Desconto { get; set; }
        public int Numero { get; set; }

        [JsonIgnore]
        public decimal ValorTotal => Valor - Desconto;

        public static Entities.Pedido Converter(PedidoRequest request, int numero)
        {
            var produtos = ProdutoRequest.Converter(request.Produtos);
            return new Entities.Pedido(request.Id, numero, request.Data, request.ClienteId, produtos, request.Valor, request.Desconto, request.ValorTotal);
        }


    }
}
