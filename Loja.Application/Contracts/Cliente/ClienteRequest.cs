using System;

namespace Loja.Application.Contract.Cliente
{
    public class ClienteRequest
    {
        public Guid? Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Aldeia { get; set; }

        public static Loja.Application.Entities.Cliente Converter(ClienteRequest request)
            => new(request.Id, request.Nome, request.Email, request.Aldeia);

    }
}
