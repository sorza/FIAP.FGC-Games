using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fgc.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class bibliotecarefactor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Biblioteca_Contas_ContaId",
                table: "Biblioteca");

            migrationBuilder.DropIndex(
                name: "IX_Biblioteca_ContaId",
                table: "Biblioteca");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Biblioteca_ContaId",
                table: "Biblioteca",
                column: "ContaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Biblioteca_Contas_ContaId",
                table: "Biblioteca",
                column: "ContaId",
                principalTable: "Contas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
