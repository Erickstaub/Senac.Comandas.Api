using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ComandasApi.Migrations
{
    /// <inheritdoc />
    public partial class usureser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "_dataHoraReserva",
                table: "Reservas",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "_dataHoraReserva",
                table: "Reservas");
        }
    }
}
