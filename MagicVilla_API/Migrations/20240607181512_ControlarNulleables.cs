using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MagicVilla_API.Migrations
{
    /// <inheritdoc />
    public partial class ControlarNulleables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "FechaActualizacion", "FechaCreacion" },
                values: new object[] { new DateTime(2024, 6, 7, 12, 15, 11, 774, DateTimeKind.Local).AddTicks(1545), new DateTime(2024, 6, 7, 12, 15, 11, 774, DateTimeKind.Local).AddTicks(1521) });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "FechaActualizacion", "FechaCreacion" },
                values: new object[] { new DateTime(2024, 6, 7, 12, 15, 11, 774, DateTimeKind.Local).AddTicks(1556), new DateTime(2024, 6, 7, 12, 15, 11, 774, DateTimeKind.Local).AddTicks(1554) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "FechaActualizacion", "FechaCreacion" },
                values: new object[] { new DateTime(2024, 6, 4, 10, 47, 51, 581, DateTimeKind.Local).AddTicks(3677), new DateTime(2024, 6, 4, 10, 47, 51, 581, DateTimeKind.Local).AddTicks(3662) });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "FechaActualizacion", "FechaCreacion" },
                values: new object[] { new DateTime(2024, 6, 4, 10, 47, 51, 581, DateTimeKind.Local).AddTicks(3682), new DateTime(2024, 6, 4, 10, 47, 51, 581, DateTimeKind.Local).AddTicks(3681) });
        }
    }
}
