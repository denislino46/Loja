using Loja.Application.Contracts.Commom;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Loja.Application.Interfaces.Services
{
    public interface IServiceBaseAsync<T, TT>
    {
        Task<ResponseModel<T>> InserirAsync(TT request);
        Task<ResponseModel<bool>> AtualizarAsync(TT request);
        Task<ResponseModel<T>> ObterAsync(Guid id);
        Task<bool> DeletarAsync(Guid id);
        Task<ResponseModel<IEnumerable<T>>> ListarAsync();
    }

    public interface IServiceBaseAsync
    {

    }
}
