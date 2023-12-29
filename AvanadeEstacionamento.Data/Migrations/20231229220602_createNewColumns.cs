using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AvanadeEstacionamento.Data.Migrations
{
    /// <inheritdoc />
    public partial class createNewColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DataCriacao",
                table: "Veiculo",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsAtivo",
                table: "Veiculo",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataCriacao",
                table: "Estacionamento",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsAtivo",
                table: "Estacionamento",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DataCriacao",
                table: "Veiculo");

            migrationBuilder.DropColumn(
                name: "IsAtivo",
                table: "Veiculo");

            migrationBuilder.DropColumn(
                name: "DataCriacao",
                table: "Estacionamento");

            migrationBuilder.DropColumn(
                name: "IsAtivo",
                table: "Estacionamento");
        }
    }
}
