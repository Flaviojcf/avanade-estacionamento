using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AvanadeEstacionamento.Data.Migrations
{
    /// <inheritdoc />
    public partial class addnewColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Veiculo_Estacionamento_EstacionamentoId",
                table: "Veiculo");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Veiculo",
                table: "Veiculo");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Estacionamento",
                table: "Estacionamento");

            migrationBuilder.RenameTable(
                name: "Veiculo",
                newName: "tb_veiculo");

            migrationBuilder.RenameTable(
                name: "Estacionamento",
                newName: "tb_estacionamento");

            migrationBuilder.RenameColumn(
                name: "IsAtivo",
                table: "tb_veiculo",
                newName: "ind_ativo");

            migrationBuilder.RenameColumn(
                name: "DataCriacao",
                table: "tb_veiculo",
                newName: "dth_cadastro");

            migrationBuilder.RenameColumn(
                name: "DataCheckout",
                table: "tb_veiculo",
                newName: "dth_checkout");

            migrationBuilder.RenameIndex(
                name: "IX_Veiculo_EstacionamentoId",
                table: "tb_veiculo",
                newName: "IX_tb_veiculo_EstacionamentoId");

            migrationBuilder.RenameColumn(
                name: "PrecoInicial",
                table: "tb_estacionamento",
                newName: "preco_inicial");

            migrationBuilder.RenameColumn(
                name: "PrecoHora",
                table: "tb_estacionamento",
                newName: "preco_hora");

            migrationBuilder.RenameColumn(
                name: "IsAtivo",
                table: "tb_estacionamento",
                newName: "ind_ativo");

            migrationBuilder.RenameColumn(
                name: "DataCriacao",
                table: "tb_estacionamento",
                newName: "dth_cadastro");

            migrationBuilder.AddColumn<DateTime>(
                name: "dth_alteracao",
                table: "tb_veiculo",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "dth_alteracao",
                table: "tb_estacionamento",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "dth_alteracao",
                table: "tb_veiculo");

            migrationBuilder.DropColumn(
                name: "dth_alteracao",
                table: "tb_estacionamento");

            migrationBuilder.RenameTable(
                name: "tb_veiculo",
                newName: "Veiculo");

            migrationBuilder.RenameTable(
                name: "tb_estacionamento",
                newName: "Estacionamento");

            migrationBuilder.RenameColumn(
                name: "ind_ativo",
                table: "Veiculo",
                newName: "IsAtivo");

            migrationBuilder.RenameColumn(
                name: "dth_checkout",
                table: "Veiculo",
                newName: "DataCheckout");

            migrationBuilder.RenameColumn(
                name: "dth_cadastro",
                table: "Veiculo",
                newName: "DataCriacao");

            migrationBuilder.RenameIndex(
                name: "IX_tb_veiculo_EstacionamentoId",
                table: "Veiculo",
                newName: "IX_Veiculo_EstacionamentoId");

            migrationBuilder.RenameColumn(
                name: "preco_inicial",
                table: "Estacionamento",
                newName: "PrecoInicial");

            migrationBuilder.RenameColumn(
                name: "preco_hora",
                table: "Estacionamento",
                newName: "PrecoHora");

            migrationBuilder.RenameColumn(
                name: "ind_ativo",
                table: "Estacionamento",
                newName: "IsAtivo");

            migrationBuilder.RenameColumn(
                name: "dth_cadastro",
                table: "Estacionamento",
                newName: "DataCriacao");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Veiculo",
                table: "Veiculo",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Estacionamento",
                table: "Estacionamento",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Veiculo_Estacionamento_EstacionamentoId",
                table: "Veiculo",
                column: "EstacionamentoId",
                principalTable: "Estacionamento",
                principalColumn: "Id");
        }
    }
}
