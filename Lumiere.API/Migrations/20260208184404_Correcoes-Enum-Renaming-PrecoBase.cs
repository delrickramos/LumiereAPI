using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lumiere.API.Migrations
{
    /// <inheritdoc />
    public partial class CorrecoesEnumRenamingPrecoBase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Desconto",
                table: "TiposIngresso");

            migrationBuilder.RenameColumn(
                name: "Versao",
                table: "Sessoes",
                newName: "Idioma");

            migrationBuilder.RenameColumn(
                name: "Preco",
                table: "Sessoes",
                newName: "PrecoBase");

            migrationBuilder.AddColumn<decimal>(
                name: "DescontoPercentual",
                table: "TiposIngresso",
                type: "decimal(5,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "Ingressos",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "TipoAssento",
                table: "Assentos",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DescontoPercentual",
                table: "TiposIngresso");

            migrationBuilder.RenameColumn(
                name: "PrecoBase",
                table: "Sessoes",
                newName: "Preco");

            migrationBuilder.RenameColumn(
                name: "Idioma",
                table: "Sessoes",
                newName: "Versao");

            migrationBuilder.AddColumn<decimal>(
                name: "Desconto",
                table: "TiposIngresso",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "Ingressos",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "TipoAssento",
                table: "Assentos",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
