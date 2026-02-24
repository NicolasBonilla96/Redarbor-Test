using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Redarbor.Persistence.Database.Migrations
{
    /// <inheritdoc />
    public partial class FixNullProperties : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_User_UserId",
                schema: "info",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_UserId",
                schema: "info",
                table: "Employees");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                schema: "info",
                table: "Employees",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<string>(
                name: "Fax",
                schema: "info",
                table: "Employees",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10);

            migrationBuilder.CreateIndex(
                name: "IX_Employees_UserId",
                schema: "info",
                table: "Employees",
                column: "UserId",
                unique: true,
                filter: "[UserId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_User_UserId",
                schema: "info",
                table: "Employees",
                column: "UserId",
                principalSchema: "auth",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_User_UserId",
                schema: "info",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_UserId",
                schema: "info",
                table: "Employees");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                schema: "info",
                table: "Employees",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Fax",
                schema: "info",
                table: "Employees",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10,
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employees_UserId",
                schema: "info",
                table: "Employees",
                column: "UserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_User_UserId",
                schema: "info",
                table: "Employees",
                column: "UserId",
                principalSchema: "auth",
                principalTable: "User",
                principalColumn: "Id");
        }
    }
}
