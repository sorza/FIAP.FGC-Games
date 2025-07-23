using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fgc.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class JogoAnoLancamento : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DataLancamento",
                table: "Jogos");

            migrationBuilder.AddColumn<int>(
                name: "AnoLancamento",
                table: "Jogos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataCriacao",
                table: "Contas",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AnoLancamento",
                table: "Jogos");

            migrationBuilder.DropColumn(
                name: "DataCriacao",
                table: "Contas");

            migrationBuilder.AddColumn<DateTime>(
                name: "DataLancamento",
                table: "Jogos",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
