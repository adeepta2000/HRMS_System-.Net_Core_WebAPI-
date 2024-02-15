using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HRMSDevelopment.Model.Migrations
{
    /// <inheritdoc />
    public partial class MigrationForEndDate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3b7fadaa-8f73-4fd1-b747-4ef662cc06c4");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "736f3466-1a96-4411-a5b5-a273b4e7003c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9c982d5d-d99a-48fc-b355-d50c839c277e");

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndDate",
                table: "EmploymentHistories",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "143c9feb-cc3b-4fe7-a022-446599fab981", "1", "Admin", "Admin" },
                    { "4e44269a-4223-4665-ada2-515c5de22ebb", "3", "Employee", "Employee" },
                    { "b6a0fb38-3569-4633-ab34-8eab6fdb3a90", "2", "HR", "HR" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "143c9feb-cc3b-4fe7-a022-446599fab981");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4e44269a-4223-4665-ada2-515c5de22ebb");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b6a0fb38-3569-4633-ab34-8eab6fdb3a90");

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndDate",
                table: "EmploymentHistories",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "3b7fadaa-8f73-4fd1-b747-4ef662cc06c4", "1", "Admin", "Admin" },
                    { "736f3466-1a96-4411-a5b5-a273b4e7003c", "2", "HR", "HR" },
                    { "9c982d5d-d99a-48fc-b355-d50c839c277e", "3", "Employee", "Employee" }
                });
        }
    }
}
