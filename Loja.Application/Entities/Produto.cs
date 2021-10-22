using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loja.Application.Entities
{
    public class Produto
    {
        public Produto() { }
        public Produto(Guid? id, string descricao, decimal valor, byte[] foto)
        {
            if (id.HasValue)
                Id = id.Value;

            Descricao = descricao;
            Valor = valor;
            Foto = foto;
        }

        public Guid Id { get; set; }
        public string Descricao { get; set; }
        public decimal Valor { get; set; }
        public byte[] Foto { get; set; }
    }
}
