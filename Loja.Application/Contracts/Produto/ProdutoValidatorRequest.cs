using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loja.Application.Contract.Produto
{
    public class ProdutoValidatorRequest : AbstractValidator<ProdutoRequest>
    {
        public ProdutoValidatorRequest()
        {
            RuleFor(x => x.Descricao).NotNull().NotEmpty().WithMessage("Informe o descrição!");
            RuleFor(x => x.Valor).GreaterThan(0).WithMessage("Valor do produto deve ser maior que zero, pois não faz sentido existir um produto com valor zero ou negativo!");
        }
    }
}
