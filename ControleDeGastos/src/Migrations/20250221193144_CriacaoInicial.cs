﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ControleDeGastos.Migrations;

/// <inheritdoc />
public partial class CriacaoInicial : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "Pessoas",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Nome = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                Idade = table.Column<int>(type: "int", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Pessoas", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "Transacoes",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Descricao = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                Valor = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                Tipo = table.Column<int>(type: "int", nullable: false),
                PessoaId = table.Column<int>(type: "int", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Transacoes", x => x.Id);
                table.ForeignKey(
                    name: "FK_Transacoes_Pessoas_PessoaId",
                    column: x => x.PessoaId,
                    principalTable: "Pessoas",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(
            name: "IX_Transacoes_PessoaId",
            table: "Transacoes",
            column: "PessoaId");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "Transacoes");

        migrationBuilder.DropTable(
            name: "Pessoas");
    }
}
