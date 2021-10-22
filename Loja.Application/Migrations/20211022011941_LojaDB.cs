using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Loja.Application.Migrations
{
    public partial class LojaDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "loja");

            migrationBuilder.CreateTable(
                name: "Clientes",
                schema: "loja",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    nome = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    email = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    aldeia = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("clienteId_PK", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Produtos",
                schema: "loja",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    descricao = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    valor = table.Column<decimal>(type: "decimal(8,5)", precision: 8, scale: 5, nullable: false),
                    urlFoto = table.Column<byte[]>(type: "varbinary(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("produtoId_PK", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Pedidos",
                schema: "loja",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    numero = table.Column<int>(type: "int", nullable: false),
                    data = table.Column<DateTime>(type: "datetime2", nullable: false),
                    clienteId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    produtos = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    valor = table.Column<decimal>(type: "decimal(8,5)", precision: 8, scale: 5, nullable: false),
                    desconto = table.Column<decimal>(type: "decimal(8,5)", precision: 8, scale: 5, nullable: false, defaultValue: 0m),
                    valorTotal = table.Column<decimal>(type: "decimal(8,5)", precision: 8, scale: 5, nullable: false, defaultValue: 0m)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pedidoId_PK", x => x.id);
                    table.ForeignKey(
                        name: "FK_Pedidos_Clientes_clienteId",
                        column: x => x.clienteId,
                        principalSchema: "loja",
                        principalTable: "Clientes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Pedidos_clienteId",
                schema: "loja",
                table: "Pedidos",
                column: "clienteId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Pedidos",
                schema: "loja");

            migrationBuilder.DropTable(
                name: "Produtos",
                schema: "loja");

            migrationBuilder.DropTable(
                name: "Clientes",
                schema: "loja");
        }
    }
}
