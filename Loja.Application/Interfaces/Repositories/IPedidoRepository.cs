using Loja.Application.Entities;
using System.Threading.Tasks;

namespace Loja.Application.Interfaces.Repositories
{
    public interface IPedidoRepository : IRepositoryBaseAsync<Pedido>
    {
        Task<int> ObterProximoNumeroAsync();
    }
}
