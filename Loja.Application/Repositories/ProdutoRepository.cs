using Loja.Application.Context;
using Loja.Application.Entities;
using Loja.Application.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loja.Application.Repositories
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly IDbContextFactory<LojaContext> _contextFactory;

        public ProdutoRepository(IDbContextFactory<LojaContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<bool> AtualizarAsync(Produto entity)
        {
            using var context = _contextFactory.CreateDbContext();
            context.Produtos.Update(entity);
            return await context.SaveChangesAsync().ConfigureAwait(false) > 0;
        }

        public async Task<bool> DeletarAsync(Produto entity)
        {
            using var context = _contextFactory.CreateDbContext();
            context.Produtos.Remove(entity);
            return await context.SaveChangesAsync().ConfigureAwait(false) > 0;
        }

        public async Task<Produto> InserirAsync(Produto entity)
        {
            using var context = _contextFactory.CreateDbContext();
            await context.Produtos.AddAsync(entity).ConfigureAwait(false);
            await context.SaveChangesAsync().ConfigureAwait(false);
            return entity;
        }

        public async Task<IEnumerable<Produto>> ListarAsync()
        {
            using var context = _contextFactory.CreateDbContext();
            return context.Produtos.ToList();
        }

        public async Task<Produto> ObterAsync(Guid id)
        {
            using var context = _contextFactory.CreateDbContext();
            return await context.Produtos.FirstOrDefaultAsync(x => x.Id == id).ConfigureAwait(false);
        }
    }
}
