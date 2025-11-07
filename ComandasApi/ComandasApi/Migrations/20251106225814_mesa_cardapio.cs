using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ComandasApi.Migrations
{
    /// <inheritdoc />
    public partial class mesa_cardapio : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "CardapioItens",
                columns: new[] { "Id", "Descricao", "PossuiPreparo", "Preco", "Titulo" },
                values: new object[,]
                {
                    { 2, "Refrigerante geladinho", false, 6.00m, "Coca cola 300ml" },
                    { 3, "Batata frita crocante", true, 12.00m, "Batata frita" }
                });

            migrationBuilder.InsertData(
                table: "Comandas",
                columns: new[] { "Id", "ClienteNome", "MesaId", "SituaçãoComanda" },
                values: new object[,]
                {
                    { 2, "Outro Cliente", 2, 0 },
                    { 3, "Terceiro Cliente", 3, 0 }
                });

            migrationBuilder.InsertData(
                table: "Mesas",
                columns: new[] { "Id", "NumeroMesa", "SituaçãoMesa" },
                values: new object[,]
                {
                    { 2, 2, 1 },
                    { 3, 3, 0 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "CardapioItens",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "CardapioItens",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Comandas",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Comandas",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Mesas",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Mesas",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
