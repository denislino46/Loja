using Loja.Application.Contract.Pedido;
using Loja.Application.Contracts.Pedido;
using Loja.Application.Interfaces;
using Loja.Application.Interfaces.Repositories;
using Loja.Application.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Loja.Application.Services
{
    public class PedidoService : IPedidoService
    {
        private readonly IPedidoRepository _repository;
        private readonly IClienteRepository _clienteRepository;
        private readonly IProdutoRepository _produtoRepository;

        public PedidoService(IPedidoRepository repository, IClienteRepository clienteRepository,
            IProdutoRepository produtoRepository)
        {
            _repository = repository;
            _clienteRepository = clienteRepository;
            _produtoRepository = produtoRepository;
        }

        public async Task<bool> AtualizarAsync(PedidoRequest request)
        {
            var validacao = new PedidoValidatorRequest().Validate(request);
            if (!validacao.IsValid)
            {
                var erros = string.Join(" | ", validacao.Errors.Select(x => x.ErrorMessage));
                throw new Exception(erros);
            }

            if ((await _repository.ObterAsync(request.Id ?? Guid.Empty).ConfigureAwait(false)) == null)
                throw new Exception("Pedido não encontrado!");

            if (request.Numero == 0)
                throw new Exception("Informe o numero do pedido!");

            if (!await ValidarClienteExiste(request.ClienteId).ConfigureAwait(false))
                throw new Exception("Cliente não encontrado!");

            if (!await ValidarProdutoExiste(request.Produtos.Select(x => x.Id ?? Guid.Empty)).ConfigureAwait(false))
                throw new Exception("Produto não encontrado!");

            var entidade = PedidoRequest.Converter(request, request.Numero);
            return await _repository.AtualizarAsync(entidade);
        }

        public async Task<bool> DeletarAsync(Guid id)
        {
            var entidade = await _repository.ObterAsync(id).ConfigureAwait(false);
            if (entidade == null)
                return false;

            return await _repository.DeletarAsync(entidade).ConfigureAwait(false);
        }

        public async Task<PedidoResponse> InserirAsync(PedidoRequest request)
        {
            var validacao = new PedidoValidatorRequest().Validate(request);
            if (!validacao.IsValid)
            {
                var erros = string.Join(" | ", validacao.Errors.Select(x => x.ErrorMessage));
                throw new Exception(erros);
            }

            if (!await ValidarClienteExiste(request.ClienteId).ConfigureAwait(false))
                throw new Exception("Cliente não encontrado!");

            if (!await ValidarProdutoExiste(request.Produtos.Select(x => x.Id ?? Guid.Empty)).ConfigureAwait(false))
                throw new Exception("Produto não encontrado!");

            var proximoNumero = await _repository.ObterProximoNumeroAsync().ConfigureAwait(false);

            var entidade = PedidoRequest.Converter(request, proximoNumero);
            await _repository.InserirAsync(entidade);
            return PedidoResponse.Converter(entidade);

        }

        public async Task<IEnumerable<PedidoResponse>> ListarAsync()
        {
            var entidades = await _repository.ListarAsync().ConfigureAwait(false);
            return PedidoResponse.Converter(entidades);
        }

        public async Task<PedidoResponse> ObterAsync(Guid id)
        {
            var entidade = await _repository.ObterAsync(id).ConfigureAwait(false);
            return PedidoResponse.Converter(entidade);
        }

        private async Task<bool> ValidarClienteExiste(Guid clienteId)
        {
            var cliente = await _clienteRepository.ObterAsync(clienteId).ConfigureAwait(false);
            return cliente != null;
        }

        private async Task<bool> ValidarProdutoExiste(IEnumerable<Guid> produtoIds)
        {
            foreach (var item in produtoIds)
            {
                var produto = await _produtoRepository.ObterAsync(item).ConfigureAwait(false);
                if (produto == null)
                    return false;
            }
            return true;
        }
    }
}
