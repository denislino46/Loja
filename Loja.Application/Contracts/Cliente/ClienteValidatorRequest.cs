using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loja.Application.Contract.Cliente
{
    public class ClienteValidatorRequest: AbstractValidator<ClienteRequest>
    {
        public ClienteValidatorRequest()
        {
            RuleFor(x => x.Nome).NotNull().NotEmpty().WithMessage("Informe o nome!");
            RuleFor(x => x.Email).EmailAddress().WithMessage("Informe o email válido!");
            RuleFor(x => x.Aldeia).NotNull().NotEmpty().WithMessage("Informe o aldeia!");
        }
    }
}
