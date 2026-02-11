using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lumiere.API.Migrations
{
    /// <inheritdoc />
    public partial class AddSalaLayout : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sessoes_Filmes_FilmeId",
                table: "Sessoes");

            migrationBuilder.DropForeignKey(
                name: "FK_Sessoes_Salas_SalaId",
                table: "Sessoes");

            migrationBuilder.AddColumn<int>(
                name: "NumeroColunas",
                table: "Salas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "NumeroLinhas",
                table: "Salas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Sessoes_Filmes_FilmeId",
                table: "Sessoes",
                column: "FilmeId",
                principalTable: "Filmes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Sessoes_Salas_SalaId",
                table: "Sessoes",
                column: "SalaId",
                principalTable: "Salas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sessoes_Filmes_FilmeId",
                table: "Sessoes");

            migrationBuilder.DropForeignKey(
                name: "FK_Sessoes_Salas_SalaId",
                table: "Sessoes");

            migrationBuilder.DropColumn(
                name: "NumeroColunas",
                table: "Salas");

            migrationBuilder.DropColumn(
                name: "NumeroLinhas",
                table: "Salas");

            migrationBuilder.AddForeignKey(
                name: "FK_Sessoes_Filmes_FilmeId",
                table: "Sessoes",
                column: "FilmeId",
                principalTable: "Filmes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Sessoes_Salas_SalaId",
                table: "Sessoes",
                column: "SalaId",
                principalTable: "Salas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
