﻿using Loja.Application.Contract.Produto;
using Loja.Application.Entities;

namespace Loja.Application.Interfaces.Services
{
    public interface IProdutoService : IServiceBaseAsync<Produto, ProdutoRequest>
    {
    }
}
