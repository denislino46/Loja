using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loja.Application.Entities
{
    public class Cliente
    {
        public Cliente() { }
        public Cliente(Guid? id, string nome, string email, string aldeia)
        {
            if (id.HasValue)
                Id = id.Value;

            Nome = nome;
            Email = email;
            Aldeia = aldeia;
        }

        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Aldeia { get; set; }

        public IList<Pedido> Pedidos { get; set; }
    }
}
