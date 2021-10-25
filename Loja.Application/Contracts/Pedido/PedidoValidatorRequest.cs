using FluentValidation;
using Loja.Application.Contract.Produto;
using Loja.Application.Interfaces;
using Loja.Application.Interfaces.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace Loja.Application.Contract.Pedido
{
    public class PedidoValidatorRequest : AbstractValidator<PedidoRequest>
    {
        public PedidoValidatorRequest(IServiceProvider serviceProvider)
        {
            var clienteRepository = serviceProvider.GetRequiredService<IClienteRepository>();
            var produtoRepository = serviceProvider.GetRequiredService<IProdutoRepository>();

            RuleFor(x => x.Produtos).NotNull().WithMessage("Informe um produto!");
            RuleFor(x => x.Data).NotNull().GreaterThan(DateTime.MinValue).WithMessage("Informe uma data válida");
            RuleFor(x => x.ClienteId).NotNull().WithMessage("Informe o clienteId");
            RuleFor(x => x.Valor).NotNull().GreaterThan(0).WithMessage("Informe o valor do pedido!");
            RuleFor(x => x.Desconto).GreaterThanOrEqualTo(0).WithMessage("Desconto não pode ser negativo!");

            RuleFor(x => x).Must(x => (clienteRepository.ObterAsync(x.ClienteId).GetAwaiter().GetResult() != null))
                .WithMessage("Cliente não encontrado!");

            RuleForEach(x => x.Produtos).SetValidator(new ProdutoValidatorRequest());

            RuleForEach(x => x.Produtos).Must(x => produtoRepository.ObterAsync(x.Id ?? Guid.Empty).GetAwaiter().GetResult() != null)
                .WithMessage("Produto não encontrado!");
            
        }
    }
}
