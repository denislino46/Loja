using FluentValidation;
using Loja.Application.Contract.Produto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loja.Application.Contract.Pedido
{
    public class PedidoValidatorRequest : AbstractValidator<PedidoRequest>
    {
        public PedidoValidatorRequest()
        {
            RuleFor(x => x.Produtos).NotNull().WithMessage("Informe um produto!");
            RuleFor(x => x.Data).NotNull().GreaterThan(DateTime.MinValue).WithMessage("Informe uma data válida");
            RuleFor(x => x.ClienteId).NotNull().WithMessage("Informe o clienteId");
            RuleFor(x => x.Valor).NotNull().GreaterThan(0).WithMessage("Informe o valor do pedido!");
            RuleFor(x => x.Desconto).GreaterThanOrEqualTo(0).WithMessage("Desconto não pode ser negativo!");

            RuleForEach(x => x.Produtos).SetValidator(new ProdutoValidatorRequest());
        }
    }
}
