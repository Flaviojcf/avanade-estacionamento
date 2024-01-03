using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AvanadeEstacionamento.Data.Migrations
{
    /// <inheritdoc />
    public partial class updateColumnName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_veiculo_tb_estacionamento_EstacionamentoId",
                table: "tb_veiculo");

            migrationBuilder.DropPrimaryKey(
                name: "PK_tb_veiculo",
                table: "tb_veiculo");

            migrationBuilder.DropPrimaryKey(
                name: "PK_tb_estacionamento",
                table: "tb_estacionamento");

            migrationBuilder.AddPrimaryKey(
                name: "veiculo_guid",
                table: "tb_veiculo",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "estacionamento_guid",
                table: "tb_estacionamento",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "estacionamento_fk_guid",
                table: "tb_veiculo",
                column: "EstacionamentoId",
                principalTable: "tb_estacionamento",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "estacionamento_fk_guid",
                table: "tb_veiculo");

            migrationBuilder.DropPrimaryKey(
                name: "veiculo_guid",
                table: "tb_veiculo");

            migrationBuilder.DropPrimaryKey(
                name: "estacionamento_guid",
                table: "tb_estacionamento");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tb_veiculo",
                table: "tb_veiculo",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tb_estacionamento",
                table: "tb_estacionamento",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_tb_veiculo_tb_estacionamento_EstacionamentoId",
                table: "tb_veiculo",
                column: "EstacionamentoId",
                principalTable: "tb_estacionamento",
                principalColumn: "Id");
        }
    }
}
