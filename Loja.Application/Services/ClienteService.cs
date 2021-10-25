using Loja.Application.Contract.Cliente;
using Loja.Application.Contracts.Commom;
using Loja.Application.Entities;
using Loja.Application.Interfaces;
using Loja.Application.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Loja.Application.Services
{
    public class ClienteService : IClienteService
    {
        private readonly IClienteRepository _repository;
        private readonly ClienteValidatorRequest _validator;
        public ClienteService(IClienteRepository repository,
            ClienteValidatorRequest validator)
        {
            _repository = repository;
            _validator = validator;
        }

        public async Task<ResponseModel<bool>> AtualizarAsync(ClienteRequest request)
        {
            var validate = await _validator.ValidateAsync(request).ConfigureAwait(false);
            if (!validate.IsValid)
                return ResponseModel<bool>.Erro(validate);

            if ((await _repository.ObterAsync(request.Id ?? Guid.Empty).ConfigureAwait(false)) == null)
            {
                var resposta = new ResponseModel<bool>();
                resposta.AdicionarErro("Cliente não encontrado!");
                return resposta;
            }

            var entidade = ClienteRequest.Converter(request);
            return ResponseModel<bool>.Sucesso(await _repository.AtualizarAsync(entidade));
        }

        public async Task<bool> DeletarAsync(Guid id)
        {
            var entidade = await _repository.ObterAsync(id).ConfigureAwait(false);
            if (entidade == null)
                return false;

            return await _repository.DeletarAsync(entidade).ConfigureAwait(false);
        }

        public async Task<ResponseModel<Cliente>> InserirAsync(ClienteRequest request)
        {
            var validate = await _validator.ValidateAsync(request).ConfigureAwait(false);
            if (!validate.IsValid)
                return ResponseModel<Cliente>.Erro(validate);

            var entidade = ClienteRequest.Converter(request);
            return ResponseModel<Cliente>.Sucesso(await _repository.InserirAsync(entidade));
        }

        public async Task<ResponseModel<IEnumerable<Cliente>>> ListarAsync()
            => ResponseModel<IEnumerable<Cliente>>.Sucesso(await _repository.ListarAsync().ConfigureAwait(false));


        public async Task<ResponseModel<Cliente>> ObterAsync(Guid id)
            => ResponseModel<Cliente>.Sucesso(await _repository.ObterAsync(id).ConfigureAwait(false));

        public async Task<bool> ValidarEmailExisteAsync(ClienteRequest request)
            => await _repository.ValidarEmailExiste(request.Id ?? Guid.Empty, request.Email).ConfigureAwait(false);
    }
}
