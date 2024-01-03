using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AvanadeEstacionamento.Data.Migrations
{
    /// <inheritdoc />
    public partial class editColumnNames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "estacionamento_fk_guid",
                table: "tb_veiculo");

            migrationBuilder.AddForeignKey(
                name: "estacionamento_guid_fk",
                table: "tb_veiculo",
                column: "EstacionamentoId",
                principalTable: "tb_estacionamento",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "estacionamento_guid_fk",
                table: "tb_veiculo");

            migrationBuilder.AddForeignKey(
                name: "estacionamento_fk_guid",
                table: "tb_veiculo",
                column: "EstacionamentoId",
                principalTable: "tb_estacionamento",
                principalColumn: "Id");
        }
    }
}
