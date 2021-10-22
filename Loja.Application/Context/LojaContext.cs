using Loja.Application.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loja.Application.Context
{
    public class LojaContext : DbContext
    {
        public LojaContext(DbContextOptions<LojaContext> options) : base(options)
        {
        }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("loja");
            ConfigurarCliente(modelBuilder);
            ConfigurarProduto(modelBuilder);
            ConfigurarPedido(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }

        void ConfigurarCliente(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cliente>(m =>
            {
                m.ToTable("Clientes");
                m.HasKey(c => c.Id).HasName("clienteId_PK");
                m.Property(c => c.Id).HasColumnName("id").ValueGeneratedOnAdd();
                m.Property(c => c.Nome).HasColumnName("nome").HasMaxLength(255).IsRequired();
                m.Property(c => c.Email).HasColumnName("email").HasMaxLength(30).IsRequired();
                m.Property(c => c.Aldeia).HasColumnName("aldeia").HasMaxLength(30).IsRequired();
            });
        }

        void ConfigurarProduto(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Produto>(m =>
            {
                m.ToTable("Produtos");
                m.HasKey(c => c.Id).HasName("produtoId_PK");
                m.Property(c => c.Id).HasColumnName("id").ValueGeneratedOnAdd();
                m.Property(c => c.Descricao).HasColumnName("descricao").HasMaxLength(255).IsRequired();
                m.Property(c => c.Valor).HasColumnName("valor").HasPrecision(8, 5).IsRequired();
                m.Property(c => c.Foto).HasColumnName("urlFoto");
            });
        }

        void ConfigurarPedido(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Pedido>(m =>
            {
                m.ToTable("Pedidos");
                m.HasKey(c => c.Id).HasName("pedidoId_PK");
                m.Property(c => c.Id).HasColumnName("id").ValueGeneratedOnAdd();
                m.Property(c => c.Data).HasColumnName("data").IsRequired();
                m.Property(c => c.ClienteId).HasColumnName("clienteId").IsRequired();
                m.Property(c => c.Produtos).HasColumnName("produtos").IsRequired();
                m.Property(c => c.Numero).HasColumnName("numero").ValueGeneratedOnAdd().ValueGeneratedOnUpdateSometimes().IsRequired();
                m.Property(c => c.Valor).HasColumnName("valor").HasPrecision(8, 5).IsRequired();
                m.Property(c => c.Desconto).HasColumnName("desconto").HasPrecision(8, 5).HasDefaultValue(0);
                m.Property(c => c.ValorTotal).HasColumnName("valorTotal").HasPrecision(8, 5).HasDefaultValue(0);

                m.HasOne(c => c.Cliente).WithMany(c => c.Pedidos);
            });
        }

    }
}
