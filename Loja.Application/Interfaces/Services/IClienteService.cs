using Loja.Application.Contract.Cliente;
using Loja.Application.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Loja.Application.Interfaces.Services
{
    public interface IClienteService : IServiceBaseAsync<Cliente, ClienteRequest>
    {
    }
}
