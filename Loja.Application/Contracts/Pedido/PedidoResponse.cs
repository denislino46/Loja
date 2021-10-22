using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace Loja.Application.Contracts.Pedido
{
    public class PedidoResponse
    {
        public Guid? Id { get; set; }
        public DateTime Data { get; set; } = DateTime.Now;
        public Guid ClienteId { get; set; }
        public List<Entities.Produto> Produtos { get; set; } = new List<Entities.Produto>();
        public decimal Valor { get; set; }
        public decimal Desconto { get; set; }
        public int Numero { get; set; }
        public decimal ValorTotal { get; set; }

        public static PedidoResponse Converter(Entities.Pedido pedido)
            => new()
            {
                Id = pedido.Id,
                Data = pedido.Data,
                ClienteId = pedido.ClienteId,
                Produtos = JsonSerializer.Deserialize<List<Entities.Produto>>(pedido.Produtos),
                Valor = pedido.Valor,
                Desconto = pedido.Desconto,
                Numero = pedido.Numero,
                ValorTotal = pedido.ValorTotal
            };
        public static List<PedidoResponse> Converter(IEnumerable<Entities.Pedido> pedidos)
        {
            var pedidosResponse = new List<PedidoResponse>();
            pedidos.ToList().ForEach(x => pedidosResponse.Add(PedidoResponse.Converter(x)));
            return pedidosResponse;
        }
    }
}
