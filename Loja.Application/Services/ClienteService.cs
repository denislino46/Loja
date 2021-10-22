using Loja.Application.Contract.Cliente;
using Loja.Application.Entities;
using Loja.Application.Interfaces;
using Loja.Application.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Loja.Application.Services
{
    public class ClienteService : IClienteService
    {
        private readonly IClienteRepository _repository;
        public ClienteService(IClienteRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> AtualizarAsync(ClienteRequest request)
        {
            var validacao = new ClienteValidatorRequest().Validate(request);
            if (!validacao.IsValid)
            {
                var erros = string.Join(" | ", validacao.Errors.Select(x => x.ErrorMessage));
                throw new Exception(erros);
            }

            if ((await _repository.ObterAsync(request.Id ?? Guid.Empty).ConfigureAwait(false)) == null)
                throw new Exception("Cliente não encontrado!");

            if (await _repository.ValidarEmailExiste(request.Id ?? Guid.Empty, request.Email).ConfigureAwait(false))
                throw new Exception("Email já cadastrado!");

            var entidade = ClienteRequest.Converter(request);
            return await _repository.AtualizarAsync(entidade);
        }

        public async Task<bool> DeletarAsync(Guid id)
        {
            var entidade = await _repository.ObterAsync(id).ConfigureAwait(false);
            if (entidade == null)
                return false;

            return await _repository.DeletarAsync(entidade).ConfigureAwait(false);
        }

        public async Task<Cliente> InserirAsync(ClienteRequest request)
        {
            var validacao = new ClienteValidatorRequest().Validate(request);
            if (!validacao.IsValid)
            {
                var erros = string.Join(" | ", validacao.Errors.Select(x => x.ErrorMessage));
                throw new Exception(erros);
            }

            if (await _repository.ValidarEmailExiste(Guid.Empty, request.Email).ConfigureAwait(false))
                throw new Exception("Email já cadastrado!");

            var entidade = ClienteRequest.Converter(request);
            return await _repository.InserirAsync(entidade);
        }

        public async Task<IEnumerable<Cliente>> ListarAsync()
            => await _repository.ListarAsync().ConfigureAwait(false);


        public async Task<Cliente> ObterAsync(Guid id)
            => await _repository.ObterAsync(id).ConfigureAwait(false);

    }
}
