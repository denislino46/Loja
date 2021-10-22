using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Loja.Application.Interfaces
{
    public interface IRepositoryBaseAsync<T>
    {
        Task<T> InserirAsync(T entity);
        Task<bool> AtualizarAsync(T entity);
        Task<T> ObterAsync(Guid id);
        Task<IEnumerable<T>> ListarAsync();
        Task<bool> DeletarAsync(T entity);
    }
}
