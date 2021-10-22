using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Loja.Application.Entities
{
    public class Pedido
    {
        public Pedido() { }

        public Pedido(Guid? id, int numero, DateTime data, Guid clienteId, List<Produto> produtos, decimal valor, decimal desconto, decimal valorTotal)
        {
            if (id.HasValue)
                Id = id.Value;

            Numero = numero;
            Data = data;
            ClienteId = clienteId;
            Valor = valor;
            Desconto = desconto;
            ValorTotal = valorTotal;

            SerializarProdutos(produtos);
        }


        public Guid Id { get; set; }
        public int Numero { get; set; }
        public DateTime Data { get; set; }
        public Guid ClienteId { get; set; }
        public string Produtos { get; set; }
        public decimal Valor { get; set; }
        public decimal Desconto { get; set; }
        public decimal ValorTotal { get; set; }

        public Cliente Cliente { get; set; }

        public void SerializarProdutos(List<Produto> produtos)
            => Produtos = JsonSerializer.Serialize(produtos);


    }
}
