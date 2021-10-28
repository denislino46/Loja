using Loja.Application.Contract.Pedido;
using Loja.Application.Contracts.Commom;
using Loja.Application.Contracts.Pedido;
using Loja.Application.Interfaces;
using Loja.Application.Interfaces.Repositories;
using Loja.Application.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Loja.Application.Services
{
    public class PedidoService : IPedidoService
    {
        private readonly IPedidoRepository _repository;
        private readonly IClienteRepository _clienteRepository;
        private readonly IProdutoRepository _produtoRepository;

        private readonly PedidoValidatorRequest _validator;

        public PedidoService(IPedidoRepository repository,
            IClienteRepository clienteRepository,
            IProdutoRepository produtoRepository,
            PedidoValidatorRequest validator)
        {
            _repository = repository;
            _clienteRepository = clienteRepository;
            _produtoRepository = produtoRepository;
            _validator = validator;
        }

        public async Task<ResponseModel<bool>> AtualizarAsync(PedidoRequest request)
        {
            var resposta = new ResponseModel<bool>();
            var validate = await _validator.ValidateAsync(request).ConfigureAwait(false);
            if (!validate.IsValid)
                return ResponseModel<bool>.Erro(validate);

            if ((await _repository.ObterAsync(request.Id ?? Guid.Empty).ConfigureAwait(false)) == null)
                resposta.AdicionarErro("Pedido não encontrado!");

            if (request.Numero == 0)
                resposta.AdicionarErro("Informe o numero do pedido!");

            if (!resposta.IsValid)
                return resposta;

            var entidade = PedidoRequest.Converter(request, request.Numero);
            return ResponseModel<bool>.Sucesso(await _repository.AtualizarAsync(entidade));
        }

        public async Task<bool> DeletarAsync(Guid id)
        {
            var entidade = await _repository.ObterAsync(id).ConfigureAwait(false);
            if (entidade == null)
                return false;

            return await _repository.DeletarAsync(entidade).ConfigureAwait(false);
        }

        public async Task<ResponseModel<PedidoResponse>> InserirAsync(PedidoRequest request)
        {
            var validate = await _validator.ValidateAsync(request).ConfigureAwait(false);
            if (!validate.IsValid)
                return ResponseModel<PedidoResponse>.Erro(validate);

            var proximoNumero = await _repository.ObterProximoNumeroAsync().ConfigureAwait(false);

            var entidade = PedidoRequest.Converter(request, proximoNumero);
            await _repository.InserirAsync(entidade);
            return ResponseModel<PedidoResponse>.Sucesso(PedidoResponse.Converter(entidade));

        }

        public async Task<ResponseModel<IEnumerable<PedidoResponse>>> ListarAsync()
        {
            var entidades = await _repository.ListarAsync().ConfigureAwait(false);
            return ResponseModel<IEnumerable<PedidoResponse>>.Sucesso(PedidoResponse.Converter(entidades));
        }

        public async Task<ResponseModel<PedidoResponse>> ObterAsync(Guid id)
        {
            var entidade = await _repository.ObterAsync(id).ConfigureAwait(false);
            return ResponseModel<PedidoResponse>.Sucesso(PedidoResponse.Converter(entidade));
        }
    }
}
