using System;
using System.Collections.Generic;

namespace Loja.Application.Contract.Produto
{
    public class ProdutoRequest
    {
        public Guid? Id { get; set; }
        public string Descricao { get; set; }
        public decimal Valor { get; set; }
        public byte[] Foto { get; set; }

        public static Entities.Produto Converter(ProdutoRequest request)
            => new(request.Id, request.Descricao, request.Valor, request.Foto);

        public static List<Entities.Produto> Converter(List<ProdutoRequest> requests)
        {
            var produtos = new List<Entities.Produto>();
            requests.ForEach(x => produtos.Add(ProdutoRequest.Converter(x)));
            return produtos;
        }

    }
}
