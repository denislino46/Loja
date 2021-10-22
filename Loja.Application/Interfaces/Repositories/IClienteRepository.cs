using Loja.Application.Entities;
using System;
using System.Threading.Tasks;

namespace Loja.Application.Interfaces
{
    public interface IClienteRepository : IRepositoryBaseAsync<Cliente>
    {
        Task<bool> ValidarEmailExiste(Guid id, string email);
    }
}
