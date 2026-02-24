using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Redarbor.Persistence.Database.Migrations
{
    /// <inheritdoc />
    public partial class AddSeedTypes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                schema: "type",
                table: "Companies",
                columns: new[] { "Id", "Name" },
                values: new object[] { 1, "RedArbor" });

            migrationBuilder.InsertData(
                schema: "type",
                table: "Portals",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Computrabajo" },
                    { 2, "Infojob" }
                });

            migrationBuilder.InsertData(
                schema: "type",
                table: "States",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Activo" },
                    { 2, "Inactivo" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "type",
                table: "Companies",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                schema: "type",
                table: "Portals",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                schema: "type",
                table: "Portals",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                schema: "type",
                table: "States",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                schema: "type",
                table: "States",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
