using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Loja.Application.Interfaces.Services
{
    public interface IServiceBaseAsync<T, TT>
    {
        Task<T> InserirAsync(TT request);
        Task<bool> AtualizarAsync(TT request);
        Task<T> ObterAsync(Guid id);
        Task<IEnumerable<T>> ListarAsync();
        Task<bool> DeletarAsync(Guid id);
    }

    public interface IServiceBaseAsync
    {

    }
}
