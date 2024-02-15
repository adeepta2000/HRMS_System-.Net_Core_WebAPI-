using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HRMSDevelopment.Model.Migrations
{
    /// <inheritdoc />
    public partial class UpdateEmployeeTableMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3dc8fafd-7e0a-4260-a005-a1df0389231d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "59aa9372-7e89-4053-b856-0bf38d3b1d3b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c4ccc541-131a-4ca0-bbdc-1db7e8baa448");

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Username",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0a6b8ab8-3ef1-4906-9d3e-82484153d06d", "2", "HR", "HR" },
                    { "7c0d9f98-b257-487e-9a14-4d1653d58500", "3", "Employee", "Employee" },
                    { "9d5c28aa-8b06-4368-a4ad-5c7988ec1d73", "1", "Admin", "Admin" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0a6b8ab8-3ef1-4906-9d3e-82484153d06d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7c0d9f98-b257-487e-9a14-4d1653d58500");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9d5c28aa-8b06-4368-a4ad-5c7988ec1d73");

            migrationBuilder.DropColumn(
                name: "Password",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "Username",
                table: "Employees");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "3dc8fafd-7e0a-4260-a005-a1df0389231d", "1", "Admin", "Admin" },
                    { "59aa9372-7e89-4053-b856-0bf38d3b1d3b", "2", "HR", "HR" },
                    { "c4ccc541-131a-4ca0-bbdc-1db7e8baa448", "3", "Employee", "Employee" }
                });
        }
    }
}
