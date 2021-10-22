﻿// <auto-generated />
using System;
using Loja.Application.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Loja.Application.Migrations
{
    [DbContext(typeof(LojaContext))]
    partial class LojaContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("loja")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.11")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Loja.Application.Entities.Cliente", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("id");

                    b.Property<string>("Aldeia")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)")
                        .HasColumnName("aldeia");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)")
                        .HasColumnName("email");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("nome");

                    b.HasKey("Id")
                        .HasName("clienteId_PK");

                    b.ToTable("Clientes");
                });

            modelBuilder.Entity("Loja.Application.Entities.Pedido", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("id");

                    b.Property<Guid>("ClienteId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("clienteId");

                    b.Property<DateTime>("Data")
                        .HasColumnType("datetime2")
                        .HasColumnName("data");

                    b.Property<decimal>("Desconto")
                        .ValueGeneratedOnAdd()
                        .HasPrecision(8, 5)
                        .HasColumnType("decimal(8,5)")
                        .HasDefaultValue(0m)
                        .HasColumnName("desconto");

                    b.Property<int>("Numero")
                        .ValueGeneratedOnUpdateSometimes()
                        .HasColumnType("int")
                        .HasColumnName("numero");

                    b.Property<string>("Produtos")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("produtos");

                    b.Property<decimal>("Valor")
                        .HasPrecision(8, 5)
                        .HasColumnType("decimal(8,5)")
                        .HasColumnName("valor");

                    b.Property<decimal>("ValorTotal")
                        .ValueGeneratedOnAdd()
                        .HasPrecision(8, 5)
                        .HasColumnType("decimal(8,5)")
                        .HasDefaultValue(0m)
                        .HasColumnName("valorTotal");

                    b.HasKey("Id")
                        .HasName("pedidoId_PK");

                    b.HasIndex("ClienteId");

                    b.ToTable("Pedidos");
                });

            modelBuilder.Entity("Loja.Application.Entities.Produto", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("id");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("descricao");

                    b.Property<byte[]>("Foto")
                        .HasColumnType("varbinary(max)")
                        .HasColumnName("urlFoto");

                    b.Property<decimal>("Valor")
                        .HasPrecision(8, 5)
                        .HasColumnType("decimal(8,5)")
                        .HasColumnName("valor");

                    b.HasKey("Id")
                        .HasName("produtoId_PK");

                    b.ToTable("Produtos");
                });

            modelBuilder.Entity("Loja.Application.Entities.Pedido", b =>
                {
                    b.HasOne("Loja.Application.Entities.Cliente", "Cliente")
                        .WithMany("Pedidos")
                        .HasForeignKey("ClienteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cliente");
                });

            modelBuilder.Entity("Loja.Application.Entities.Cliente", b =>
                {
                    b.Navigation("Pedidos");
                });
#pragma warning restore 612, 618
        }
    }
}
