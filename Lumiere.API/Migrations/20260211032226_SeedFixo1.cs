using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Lumiere.API.Migrations
{
    /// <inheritdoc />
    public partial class SeedFixo1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "FormatosSessao",
                columns: new[] { "Id", "Nome" },
                values: new object[,]
                {
                    { 1, "2D" },
                    { 2, "3D" },
                    { 3, "IMAX" }
                });

            migrationBuilder.InsertData(
                table: "Generos",
                columns: new[] { "Id", "Nome" },
                values: new object[,]
                {
                    { 1, "Ação" },
                    { 2, "Comédia" },
                    { 3, "Drama" },
                    { 4, "Terror" },
                    { 5, "Ficção" },
                    { 6, "Animação" }
                });

            migrationBuilder.InsertData(
                table: "TiposIngresso",
                columns: new[] { "Id", "DescontoPercentual", "Nome" },
                values: new object[,]
                {
                    { 1, 0.00m, "Inteira" },
                    { 2, 50.00m, "Meia" },
                    { 3, 50.00m, "Estudante" },
                    { 4, 50.00m, "Idoso" },
                    { 5, 30.00m, "Criança" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "FormatosSessao",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "FormatosSessao",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "FormatosSessao",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Generos",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Generos",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Generos",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Generos",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Generos",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Generos",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "TiposIngresso",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "TiposIngresso",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "TiposIngresso",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "TiposIngresso",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "TiposIngresso",
                keyColumn: "Id",
                keyValue: 5);
        }
    }
}
