using Loja.Application.Contract.Produto;
using Loja.Application.Entities;
using Loja.Application.Interfaces.Repositories;
using Loja.Application.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loja.Application.Services
{
    public class ProdutoService : IProdutoService
    {
        private readonly IProdutoRepository _repository;
        public ProdutoService(IProdutoRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> AtualizarAsync(ProdutoRequest request)
        {
            var validacao = new ProdutoValidatorRequest().Validate(request);
            if (!validacao.IsValid)
            {
                var erros = string.Join(" | ", validacao.Errors.Select(x => x.ErrorMessage));
                throw new Exception(erros);
            }

            if ((await _repository.ObterAsync(request.Id ?? Guid.Empty).ConfigureAwait(false)) == null)
                throw new Exception("Produto não encontrado!");

            var entidade = ProdutoRequest.Converter(request);
            return await _repository.AtualizarAsync(entidade);
        }

        public async Task<bool> DeletarAsync(Guid id)
        {
            var entidade = await _repository.ObterAsync(id).ConfigureAwait(false);
            if (entidade == null)
                return false;

            return await _repository.DeletarAsync(entidade).ConfigureAwait(false);
        }

        public async Task<Produto> InserirAsync(ProdutoRequest request)
        {
            var validacao = new ProdutoValidatorRequest().Validate(request);
            if (!validacao.IsValid)
            {
                var erros = string.Join(" | ", validacao.Errors.Select(x => x.ErrorMessage));
                throw new Exception(erros);
            }

            var entidade = ProdutoRequest.Converter(request);
            return await _repository.InserirAsync(entidade);
        }

        public async Task<IEnumerable<Produto>> ListarAsync()
            => await _repository.ListarAsync().ConfigureAwait(false);


        public async Task<Produto> ObterAsync(Guid id)
            => await _repository.ObterAsync(id).ConfigureAwait(false);
    }
}
