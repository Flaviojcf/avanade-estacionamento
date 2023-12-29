using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AvanadeEstacionamento.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Estacionamento",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PrecoInicial = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PrecoHora = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Estacionamento", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Veiculo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Placa = table.Column<string>(type: "varchar(max)", unicode: false, nullable: false),
                    EstacionamentoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Veiculo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Veiculo_Estacionamento_EstacionamentoId",
                        column: x => x.EstacionamentoId,
                        principalTable: "Estacionamento",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Veiculo_EstacionamentoId",
                table: "Veiculo",
                column: "EstacionamentoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Veiculo");

            migrationBuilder.DropTable(
                name: "Estacionamento");
        }
    }
}
