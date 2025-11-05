using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ComandasApi.Migrations
{
    /// <inheritdoc />
    public partial class v3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "CardapioItens",
                columns: new[] { "Id", "Descricao", "PossuiPreparo", "Preco", "Titulo" },
                values: new object[] { 1, "que x tudo que x bacon...", true, 19.90m, "X bacon" });

            migrationBuilder.InsertData(
                table: "Comandas",
                columns: new[] { "Id", "ClienteNome", "MesaId", "SituaçãoComanda" },
                values: new object[] { 1, "Cliente Exemplo", 1, 1 });

            migrationBuilder.InsertData(
                table: "Mesas",
                columns: new[] { "Id", "NumeroMesa", "SituaçãoMesa" },
                values: new object[] { 1, 1, 0 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "CardapioItens",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Comandas",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Mesas",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
