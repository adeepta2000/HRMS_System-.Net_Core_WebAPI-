using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HRMSDevelopment.Model.Migrations
{
    /// <inheritdoc />
    public partial class MigrationForBonusManagementModule : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0f9f5c57-1fad-4ccd-9f5c-63744af617ad", "2", "HR", "HR" },
                    { "26cc9778-6b1e-4b68-9c12-b5eb23cc0b40", "1", "Admin", "Admin" },
                    { "ec4017bf-69fa-4966-8bea-1c58c2276bbb", "3", "Employee", "Employee" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0f9f5c57-1fad-4ccd-9f5c-63744af617ad");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "26cc9778-6b1e-4b68-9c12-b5eb23cc0b40");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ec4017bf-69fa-4966-8bea-1c58c2276bbb");

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
    }
}
