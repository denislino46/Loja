using Loja.Application.Contract.Cliente;
using Loja.Application.Contract.Pedido;
using Loja.Application.Contract.Produto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loja.Application.Contracts.Ioc
{
    public static class IocValidators
    {
        public static List<Type> ObterSingleTypes()
        {
            return new List<Type>
            {
                typeof(ClienteValidatorRequest),
                typeof(ProdutoValidatorRequest),
                typeof(PedidoValidatorRequest)
            };
        }
    }
}
