using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HRMSDevelopment.Model.Migrations
{
    /// <inheritdoc />
    public partial class MigrationForSalaryManagementModule : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6769602b-c6b6-4b10-aba8-86271ebf9e36");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d0907a03-7c53-4e34-89e6-e0a72b11fa3c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e063bc9c-9ea3-4af8-b152-15d0ac46aead");

            migrationBuilder.DropColumn(
                name: "Bonus",
                table: "Salaries");

            migrationBuilder.DropColumn(
                name: "Deduction",
                table: "Salaries");

            migrationBuilder.DropColumn(
                name: "RevisionDate",
                table: "Salaries");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Salaries",
                newName: "SalaryId");

            migrationBuilder.CreateTable(
                name: "Bonus",
                columns: table => new
                {
                    BonusId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SalaryId = table.Column<int>(type: "int", nullable: false),
                    BonusAmmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    BonusDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bonus", x => x.BonusId);
                    table.ForeignKey(
                        name: "FK_Bonus_Salaries_SalaryId",
                        column: x => x.SalaryId,
                        principalTable: "Salaries",
                        principalColumn: "SalaryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Deductions",
                columns: table => new
                {
                    DeductionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SalaryId = table.Column<int>(type: "int", nullable: false),
                    DeductionAmmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DeductionDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Deductions", x => x.DeductionId);
                    table.ForeignKey(
                        name: "FK_Deductions_Salaries_SalaryId",
                        column: x => x.SalaryId,
                        principalTable: "Salaries",
                        principalColumn: "SalaryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SalaryRevisions",
                columns: table => new
                {
                    RevisionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SalaryId = table.Column<int>(type: "int", nullable: false),
                    RevisedSalary = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    RevisionDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalaryRevisions", x => x.RevisionId);
                    table.ForeignKey(
                        name: "FK_SalaryRevisions_Salaries_SalaryId",
                        column: x => x.SalaryId,
                        principalTable: "Salaries",
                        principalColumn: "SalaryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "385d56d1-7c32-44e5-b75b-4853c364b68b", "3", "Employee", "Employee" },
                    { "9c3c14bf-2a5c-4fd5-9924-d3406f4031c4", "2", "HR", "HR" },
                    { "add0b3f2-1130-4929-be24-e1f94b229658", "1", "Admin", "Admin" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bonus_SalaryId",
                table: "Bonus",
                column: "SalaryId");

            migrationBuilder.CreateIndex(
                name: "IX_Deductions_SalaryId",
                table: "Deductions",
                column: "SalaryId");

            migrationBuilder.CreateIndex(
                name: "IX_SalaryRevisions_SalaryId",
                table: "SalaryRevisions",
                column: "SalaryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bonus");

            migrationBuilder.DropTable(
                name: "Deductions");

            migrationBuilder.DropTable(
                name: "SalaryRevisions");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "385d56d1-7c32-44e5-b75b-4853c364b68b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9c3c14bf-2a5c-4fd5-9924-d3406f4031c4");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "add0b3f2-1130-4929-be24-e1f94b229658");

            migrationBuilder.RenameColumn(
                name: "SalaryId",
                table: "Salaries",
                newName: "Id");

            migrationBuilder.AddColumn<decimal>(
                name: "Bonus",
                table: "Salaries",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Deduction",
                table: "Salaries",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<DateTime>(
                name: "RevisionDate",
                table: "Salaries",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "6769602b-c6b6-4b10-aba8-86271ebf9e36", "2", "HR", "HR" },
                    { "d0907a03-7c53-4e34-89e6-e0a72b11fa3c", "1", "Admin", "Admin" },
                    { "e063bc9c-9ea3-4af8-b152-15d0ac46aead", "3", "Employee", "Employee" }
                });
        }
    }
}
