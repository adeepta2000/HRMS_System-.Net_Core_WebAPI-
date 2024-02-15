using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HRMSDevelopment.Model.Migrations
{
    /// <inheritdoc />
    public partial class MigrationForBonusManagementModule2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateTable(
                name: "BonusCriterias",
                columns: table => new
                {
                    BonusCriteriaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CriteriaName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CriteriaDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ValidFrom = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BonusCriterias", x => x.BonusCriteriaId);
                });

            migrationBuilder.CreateTable(
                name: "BonusEligibilities",
                columns: table => new
                {
                    BonusEligibilityId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    BonusCriteriaId = table.Column<int>(type: "int", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BonusEligibilities", x => x.BonusEligibilityId);
                    table.ForeignKey(
                        name: "FK_BonusEligibilities_BonusCriterias_BonusCriteriaId",
                        column: x => x.BonusCriteriaId,
                        principalTable: "BonusCriterias",
                        principalColumn: "BonusCriteriaId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BonusEligibilities_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BonusDistributions",
                columns: table => new
                {
                    BonusDistributionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BonusEligibilityId = table.Column<int>(type: "int", nullable: false),
                    BonusAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DistributionDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BonusDistributions", x => x.BonusDistributionId);
                    table.ForeignKey(
                        name: "FK_BonusDistributions_BonusEligibilities_BonusEligibilityId",
                        column: x => x.BonusEligibilityId,
                        principalTable: "BonusEligibilities",
                        principalColumn: "BonusEligibilityId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "91a57178-9f2b-4ae3-889f-0b0d0f1c16b2", "2", "HR", "HR" },
                    { "c209d745-dcf7-415c-a449-0d759e33f1b2", "1", "Admin", "Admin" },
                    { "e3af3570-f64a-4592-878b-69e00484f253", "3", "Employee", "Employee" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_BonusDistributions_BonusEligibilityId",
                table: "BonusDistributions",
                column: "BonusEligibilityId");

            migrationBuilder.CreateIndex(
                name: "IX_BonusEligibilities_BonusCriteriaId",
                table: "BonusEligibilities",
                column: "BonusCriteriaId");

            migrationBuilder.CreateIndex(
                name: "IX_BonusEligibilities_EmployeeId",
                table: "BonusEligibilities",
                column: "EmployeeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BonusDistributions");

            migrationBuilder.DropTable(
                name: "BonusEligibilities");

            migrationBuilder.DropTable(
                name: "BonusCriterias");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "91a57178-9f2b-4ae3-889f-0b0d0f1c16b2");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c209d745-dcf7-415c-a449-0d759e33f1b2");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e3af3570-f64a-4592-878b-69e00484f253");

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
    }
}
