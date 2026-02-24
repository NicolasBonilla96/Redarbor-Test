using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Redarbor.Persistence.Database.Migrations
{
    /// <inheritdoc />
    public partial class AddTypesAndInfoEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "type");

            migrationBuilder.EnsureSchema(
                name: "info");

            migrationBuilder.CreateTable(
                name: "Companies",
                schema: "type",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Portals",
                schema: "type",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Portals", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "States",
                schema: "type",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_States", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                schema: "info",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    Fax = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    PortalId = table.Column<int>(type: "int", nullable: false),
                    StateId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Employees_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalSchema: "type",
                        principalTable: "Companies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Employees_Portals_PortalId",
                        column: x => x.PortalId,
                        principalSchema: "type",
                        principalTable: "Portals",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Employees_States_StateId",
                        column: x => x.StateId,
                        principalSchema: "type",
                        principalTable: "States",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Employees_User_UserId",
                        column: x => x.UserId,
                        principalSchema: "auth",
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Employees_CompanyId",
                schema: "info",
                table: "Employees",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_PortalId",
                schema: "info",
                table: "Employees",
                column: "PortalId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_StateId",
                schema: "info",
                table: "Employees",
                column: "StateId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_UserId",
                schema: "info",
                table: "Employees",
                column: "UserId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Employees",
                schema: "info");

            migrationBuilder.DropTable(
                name: "Companies",
                schema: "type");

            migrationBuilder.DropTable(
                name: "Portals",
                schema: "type");

            migrationBuilder.DropTable(
                name: "States",
                schema: "type");
        }
    }
}
