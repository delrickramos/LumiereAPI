using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lumiere.API.Migrations
{
    /// <inheritdoc />
    public partial class AddNomeToAssento : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Nome",
                table: "Assentos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Nome",
                table: "Assentos");
        }
    }
}
