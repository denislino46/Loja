using FluentValidation;
using FluentValidation.Results;
using Loja.Application.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace Loja.Application.Contract.Cliente
{
    public class ClienteValidatorRequest : AbstractValidator<ClienteRequest>
    {
        //private readonly IServiceProvider _serviceProvider;
        public ClienteValidatorRequest(IServiceProvider _serviceProvider)
        {
            var repository = _serviceProvider.GetRequiredService<IClienteRepository>();

            RuleFor(x => x.Nome).NotNull().NotEmpty().WithMessage("Informe o nome!");
            RuleFor(x => x.Email).EmailAddress().WithMessage("Informe o email válido!");
            RuleFor(x => x.Aldeia).NotNull().NotEmpty().WithMessage("Informe o aldeia!");

            RuleFor(x => x).Must(x => !(repository.ValidarEmailExiste(x.Id ?? Guid.Empty, x.Email).GetAwaiter().GetResult())).WithMessage("Email já cadastrado!");
        }
        public ClienteValidatorRequest()
        {

        }
    }
}
