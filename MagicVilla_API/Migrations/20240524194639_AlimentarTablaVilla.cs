using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MagicVilla_API.Migrations
{
    /// <inheritdoc />
    public partial class AlimentarTablaVilla : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Villas",
                columns: new[] { "Id", "Amenidad", "Detalle", "FechaActualizacion", "FechaCreacion", "ImageUrl", "MetrosCuadrados", "Nombre", "Ocupantes", "Tarifa" },
                values: new object[,]
                {
                    { 1, "Nadar", "Lago Hermoso", new DateTime(2024, 5, 24, 13, 46, 39, 23, DateTimeKind.Local).AddTicks(31), new DateTime(2024, 5, 24, 13, 46, 39, 23, DateTimeKind.Local).AddTicks(18), "", 5000, "Villa Zirahuen", 0, 500.0 },
                    { 2, "Nadar contracorriente", "Rio Hermoso", new DateTime(2024, 5, 24, 13, 46, 39, 23, DateTimeKind.Local).AddTicks(35), new DateTime(2024, 5, 24, 13, 46, 39, 23, DateTimeKind.Local).AddTicks(34), "", 1000, "Villa Tarejero", 0, 200.0 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
