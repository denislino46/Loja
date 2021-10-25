using Loja.Application.Contract.Cliente;
using Loja.Application.Contracts.Commom;
using Loja.Application.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Loja.Application.Interfaces.Services
{
    public interface IClienteService : IServiceBaseAsync<Cliente, ClienteRequest>
    {
        Task<bool> ValidarEmailExisteAsync(ClienteRequest request);
    }
}
