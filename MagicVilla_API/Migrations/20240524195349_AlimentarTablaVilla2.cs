using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MagicVilla_API.Migrations
{
    /// <inheritdoc />
    public partial class AlimentarTablaVilla2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "FechaActualizacion", "FechaCreacion", "Ocupantes" },
                values: new object[] { new DateTime(2024, 5, 24, 13, 53, 49, 209, DateTimeKind.Local).AddTicks(2808), new DateTime(2024, 5, 24, 13, 53, 49, 209, DateTimeKind.Local).AddTicks(2794), 1000 });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "FechaActualizacion", "FechaCreacion", "Ocupantes" },
                values: new object[] { new DateTime(2024, 5, 24, 13, 53, 49, 209, DateTimeKind.Local).AddTicks(2812), new DateTime(2024, 5, 24, 13, 53, 49, 209, DateTimeKind.Local).AddTicks(2812), 300 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "FechaActualizacion", "FechaCreacion", "Ocupantes" },
                values: new object[] { new DateTime(2024, 5, 24, 13, 46, 39, 23, DateTimeKind.Local).AddTicks(31), new DateTime(2024, 5, 24, 13, 46, 39, 23, DateTimeKind.Local).AddTicks(18), 0 });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "FechaActualizacion", "FechaCreacion", "Ocupantes" },
                values: new object[] { new DateTime(2024, 5, 24, 13, 46, 39, 23, DateTimeKind.Local).AddTicks(35), new DateTime(2024, 5, 24, 13, 46, 39, 23, DateTimeKind.Local).AddTicks(34), 0 });
        }
    }
}
