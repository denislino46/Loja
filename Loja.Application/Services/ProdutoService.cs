using Loja.Application.Contract.Produto;
using Loja.Application.Contracts.Commom;
using Loja.Application.Entities;
using Loja.Application.Interfaces.Repositories;
using Loja.Application.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Loja.Application.Services
{
    public class ProdutoService : IProdutoService
    {
        private readonly IProdutoRepository _repository;
        private readonly ProdutoValidatorRequest _validator;
        public ProdutoService(IProdutoRepository repository,
            ProdutoValidatorRequest validator)
        {
            _repository = repository;
            _validator = validator;
        }

        public async Task<ResponseModel<bool>> AtualizarAsync(ProdutoRequest request)
        {
            var validate = await _validator.ValidateAsync(request).ConfigureAwait(false);
            if (!validate.IsValid)
                return ResponseModel<bool>.Erro(validate);

            if ((await _repository.ObterAsync(request.Id ?? Guid.Empty).ConfigureAwait(false)) == null)
            {
                var resposta = new ResponseModel<bool>();
                resposta.AdicionarErro("Produto não encontrado!");
                return resposta;
            }

            var entidade = ProdutoRequest.Converter(request);
            return ResponseModel<bool>.Sucesso(await _repository.AtualizarAsync(entidade));
        }

        public async Task<bool> DeletarAsync(Guid id)
        {
            var entidade = await _repository.ObterAsync(id).ConfigureAwait(false);
            if (entidade == null)
                return false;

            return await _repository.DeletarAsync(entidade).ConfigureAwait(false);
        }

        public async Task<ResponseModel<Produto>> InserirAsync(ProdutoRequest request)
        {
            var validate = await _validator.ValidateAsync(request).ConfigureAwait(false);
            if (!validate.IsValid)
                return ResponseModel<Produto>.Erro(validate);

            var entidade = ProdutoRequest.Converter(request);
            return ResponseModel<Produto>.Sucesso(await _repository.InserirAsync(entidade));
        }

        public async Task<ResponseModel<IEnumerable<Produto>>> ListarAsync()
            => ResponseModel<IEnumerable<Produto>>.Sucesso(await _repository.ListarAsync().ConfigureAwait(false));


        public async Task<ResponseModel<Produto>> ObterAsync(Guid id)
            => ResponseModel<Produto>.Sucesso(await _repository.ObterAsync(id).ConfigureAwait(false));
    }
}
