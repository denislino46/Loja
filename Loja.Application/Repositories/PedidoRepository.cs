using Loja.Application.Context;
using Loja.Application.Entities;
using Loja.Application.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Loja.Application.Repositories
{
    public class PedidoRepository : IPedidoRepository
    {
        private readonly IDbContextFactory<LojaContext> _contextFactory;

        public PedidoRepository(IDbContextFactory<LojaContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<bool> AtualizarAsync(Pedido entity)
        {
            using var context = _contextFactory.CreateDbContext();
            context.Pedidos.Update(entity);
            return await context.SaveChangesAsync().ConfigureAwait(false) > 0;
        }

        public async Task<bool> DeletarAsync(Pedido entity)
        {
            using var context = _contextFactory.CreateDbContext();
            context.Pedidos.Remove(entity);
            return await context.SaveChangesAsync().ConfigureAwait(false) > 0;
        }

        public async Task<Pedido> InserirAsync(Pedido entity)
        {
            using var context = _contextFactory.CreateDbContext();
            await context.Pedidos.AddAsync(entity).ConfigureAwait(false);
            await context.SaveChangesAsync().ConfigureAwait(false);
            return entity;
        }

        public async Task<IEnumerable<Pedido>> ListarAsync()
        {
            using var context = _contextFactory.CreateDbContext();
            return context.Pedidos.ToList();
        }

        public async Task<Pedido> ObterAsync(Guid id)
        {
            using var context = _contextFactory.CreateDbContext();
            return await context.Pedidos.FirstOrDefaultAsync(x => x.Id == id).ConfigureAwait(false);
        }

        public async Task<int> ObterProximoNumeroAsync()
        {
            var pedidos = await ListarAsync().ConfigureAwait(false) ?? new List<Pedido>();
            if (!pedidos.Any())
                return 1;

            return pedidos.Max(x => x.Numero) + 1;
        }
    }
}
