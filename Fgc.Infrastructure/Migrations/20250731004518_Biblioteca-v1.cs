using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fgc.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Bibliotecav1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Biblioteca",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ContaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataAlteracao = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Biblioteca", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Biblioteca_Contas_ContaId",
                        column: x => x.ContaId,
                        principalTable: "Contas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BibliotecaJogo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BibliotecaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    JogoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DataAquisicao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ValorPago = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataAlteracao = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BibliotecaJogo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BibliotecaJogo_Biblioteca_BibliotecaId",
                        column: x => x.BibliotecaId,
                        principalTable: "Biblioteca",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BibliotecaJogo_Jogos_JogoId",
                        column: x => x.JogoId,
                        principalTable: "Jogos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Biblioteca_ContaId",
                table: "Biblioteca",
                column: "ContaId");

            migrationBuilder.CreateIndex(
                name: "IX_BibliotecaJogo_BibliotecaId",
                table: "BibliotecaJogo",
                column: "BibliotecaId");

            migrationBuilder.CreateIndex(
                name: "IX_BibliotecaJogo_JogoId",
                table: "BibliotecaJogo",
                column: "JogoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BibliotecaJogo");

            migrationBuilder.DropTable(
                name: "Biblioteca");
        }
    }
}
