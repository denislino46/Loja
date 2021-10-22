using Loja.Application.Context;
using Loja.Application.Entities;
using Loja.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loja.Application.Repositories
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly IDbContextFactory<LojaContext> _contextFactory;

        public ClienteRepository(IDbContextFactory<LojaContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<bool> AtualizarAsync(Cliente entity)
        {
            using var context = _contextFactory.CreateDbContext();
            context.Clientes.Update(entity);
            return await context.SaveChangesAsync().ConfigureAwait(false) > 0;
        }

        public async Task<bool> DeletarAsync(Cliente entity)
        {
            using var context = _contextFactory.CreateDbContext();
            context.Clientes.Remove(entity);
            return await context.SaveChangesAsync().ConfigureAwait(false) > 0;
        }

        public async Task<Cliente> InserirAsync(Cliente entity)
        {
            using var context = _contextFactory.CreateDbContext();
            await context.Clientes.AddAsync(entity).ConfigureAwait(false);
            await context.SaveChangesAsync().ConfigureAwait(false);
            return entity;
        }

        public async Task<IEnumerable<Cliente>> ListarAsync()
        {
            using var context = _contextFactory.CreateDbContext();
            return context.Clientes.ToList();
        }

        public async Task<Cliente> ObterAsync(Guid id)
        {
            using var context = _contextFactory.CreateDbContext();
            return await context.Clientes.FirstOrDefaultAsync(x => x.Id == id).ConfigureAwait(false);
        }

        public async Task<bool> ValidarEmailExiste(Guid id, string email)
        {
            using var context = _contextFactory.CreateDbContext();
            return await context.Clientes.AnyAsync(x => x.Id != id && x.Email.ToLower() == email.ToLower()).ConfigureAwait(false);
        }
    }
}
