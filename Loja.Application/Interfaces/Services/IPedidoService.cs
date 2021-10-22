using Loja.Application.Contract.Pedido;
using Loja.Application.Contracts.Pedido;

namespace Loja.Application.Interfaces.Services
{
    public interface IPedidoService : IServiceBaseAsync<PedidoResponse, PedidoRequest>
    {
    }
}
