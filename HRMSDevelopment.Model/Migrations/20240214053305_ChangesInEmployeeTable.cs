using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HRMSDevelopment.Model.Migrations
{
    /// <inheritdoc />
    public partial class ChangesInEmployeeTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "79a485ad-3942-4685-bdbd-e47a72077e26");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8ea63114-f1a7-42a0-97e7-57c9b90cc65a");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a37a7d81-50e1-417e-8b55-69736a486bf8");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Employees",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0c28d5e8-bb54-421d-abbe-a456f175646a", "1", "Admin", "Admin" },
                    { "3fed45b6-3789-4245-98dc-ab0f153e7d69", "2", "HR", "HR" },
                    { "f4505c3e-4854-439c-bf4b-df2535d66c1e", "3", "Employee", "Employee" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0c28d5e8-bb54-421d-abbe-a456f175646a");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3fed45b6-3789-4245-98dc-ab0f153e7d69");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f4505c3e-4854-439c-bf4b-df2535d66c1e");

            migrationBuilder.DropColumn(
                name: "Address",
                table: "Employees");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "79a485ad-3942-4685-bdbd-e47a72077e26", "2", "HR", "HR" },
                    { "8ea63114-f1a7-42a0-97e7-57c9b90cc65a", "1", "Admin", "Admin" },
                    { "a37a7d81-50e1-417e-8b55-69736a486bf8", "3", "Employee", "Employee" }
                });
        }
    }
}
