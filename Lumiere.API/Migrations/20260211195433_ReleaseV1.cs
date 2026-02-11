using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Lumiere.API.Migrations
{
    /// <inheritdoc />
    public partial class ReleaseV1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FormatosSessao",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormatosSessao", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Generos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Generos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Salas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    NumeroLinhas = table.Column<int>(type: "int", nullable: false),
                    NumeroColunas = table.Column<int>(type: "int", nullable: false),
                    Capacidade = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Salas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TiposIngresso",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    DescontoPercentual = table.Column<decimal>(type: "decimal(5,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TiposIngresso", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Filmes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titulo = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    DuracaoMinutos = table.Column<int>(type: "int", nullable: false),
                    ClassificacaoIndicativa = table.Column<int>(type: "int", nullable: false),
                    Sinopse = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    Direcao = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                    Distribuidora = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    GeneroId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Filmes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Filmes_Generos_GeneroId",
                        column: x => x.GeneroId,
                        principalTable: "Generos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Assentos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Coluna = table.Column<int>(type: "int", nullable: false),
                    Fileira = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    TipoAssento = table.Column<int>(type: "int", nullable: false),
                    SalaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assentos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Assentos_Salas_SalaId",
                        column: x => x.SalaId,
                        principalTable: "Salas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Sessoes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DataHoraInicio = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    DataHoraFim = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    Idioma = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    PrecoBase = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SalaId = table.Column<int>(type: "int", nullable: false),
                    FormatoSessaoId = table.Column<int>(type: "int", nullable: false),
                    FilmeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sessoes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sessoes_Filmes_FilmeId",
                        column: x => x.FilmeId,
                        principalTable: "Filmes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Sessoes_FormatosSessao_FormatoSessaoId",
                        column: x => x.FormatoSessaoId,
                        principalTable: "FormatosSessao",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Sessoes_Salas_SalaId",
                        column: x => x.SalaId,
                        principalTable: "Salas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Ingressos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PrecoFinal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    SessaoId = table.Column<int>(type: "int", nullable: false),
                    AssentoId = table.Column<int>(type: "int", nullable: false),
                    TipoIngressoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ingressos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ingressos_Assentos_AssentoId",
                        column: x => x.AssentoId,
                        principalTable: "Assentos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Ingressos_Sessoes_SessaoId",
                        column: x => x.SessaoId,
                        principalTable: "Sessoes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ingressos_TiposIngresso_TipoIngressoId",
                        column: x => x.TipoIngressoId,
                        principalTable: "TiposIngresso",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_Assentos_SalaId",
                table: "Assentos",
                column: "SalaId");

            migrationBuilder.CreateIndex(
                name: "IX_Filmes_GeneroId",
                table: "Filmes",
                column: "GeneroId");

            migrationBuilder.CreateIndex(
                name: "IX_Ingressos_AssentoId",
                table: "Ingressos",
                column: "AssentoId");

            migrationBuilder.CreateIndex(
                name: "IX_Ingressos_SessaoId",
                table: "Ingressos",
                column: "SessaoId");

            migrationBuilder.CreateIndex(
                name: "IX_Ingressos_TipoIngressoId",
                table: "Ingressos",
                column: "TipoIngressoId");

            migrationBuilder.CreateIndex(
                name: "IX_Sessoes_FilmeId",
                table: "Sessoes",
                column: "FilmeId");

            migrationBuilder.CreateIndex(
                name: "IX_Sessoes_FormatoSessaoId",
                table: "Sessoes",
                column: "FormatoSessaoId");

            migrationBuilder.CreateIndex(
                name: "IX_Sessoes_SalaId",
                table: "Sessoes",
                column: "SalaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ingressos");

            migrationBuilder.DropTable(
                name: "Assentos");

            migrationBuilder.DropTable(
                name: "Sessoes");

            migrationBuilder.DropTable(
                name: "TiposIngresso");

            migrationBuilder.DropTable(
                name: "Filmes");

            migrationBuilder.DropTable(
                name: "FormatosSessao");

            migrationBuilder.DropTable(
                name: "Salas");

            migrationBuilder.DropTable(
                name: "Generos");
        }
    }
}
