using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ControleDeGastos.Migrations
{
    /// <inheritdoc />
    public partial class AdicionarCampoDataECategoria : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Categoria",
                table: "Transacoes",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateOnly>(
                name: "Data",
                table: "Transacoes",
                type: "Date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Categoria",
                table: "Transacoes");

            migrationBuilder.DropColumn(
                name: "Data",
                table: "Transacoes");
        }
    }
}
