using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HRMSDevelopment.Model.Migrations
{
    /// <inheritdoc />
    public partial class BonusDistributionTableChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BonusDistributions_BonusEligibility_BonusEligibilityId",
                table: "BonusDistributions");

            migrationBuilder.DropTable(
                name: "BonusEligibility");

            migrationBuilder.DropIndex(
                name: "IX_BonusDistributions_BonusEligibilityId",
                table: "BonusDistributions");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0ce51e75-a671-4e71-8ee3-3f6c792004e5");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ae0c9a98-bd2e-4a14-8200-4729c44390cc");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c829303d-c5f9-448f-adbb-6e281e54ea9c");

            migrationBuilder.DropColumn(
                name: "BonusEligibilityId",
                table: "BonusDistributions");

            migrationBuilder.AddColumn<int>(
                name: "EmployeeId",
                table: "BonusDistributions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "79a485ad-3942-4685-bdbd-e47a72077e26", "2", "HR", "HR" },
                    { "8ea63114-f1a7-42a0-97e7-57c9b90cc65a", "1", "Admin", "Admin" },
                    { "a37a7d81-50e1-417e-8b55-69736a486bf8", "3", "Employee", "Employee" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_BonusDistributions_EmployeeId",
                table: "BonusDistributions",
                column: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_BonusDistributions_Employees_EmployeeId",
                table: "BonusDistributions",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "EmployeeId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BonusDistributions_Employees_EmployeeId",
                table: "BonusDistributions");

            migrationBuilder.DropIndex(
                name: "IX_BonusDistributions_EmployeeId",
                table: "BonusDistributions");

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

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "BonusDistributions");

            migrationBuilder.AddColumn<int>(
                name: "BonusEligibilityId",
                table: "BonusDistributions",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "BonusEligibility",
                columns: table => new
                {
                    BonusEligibilityId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BonusCriteriaId = table.Column<int>(type: "int", nullable: false),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BonusEligibility", x => x.BonusEligibilityId);
                    table.ForeignKey(
                        name: "FK_BonusEligibility_BonusCriterias_BonusCriteriaId",
                        column: x => x.BonusCriteriaId,
                        principalTable: "BonusCriterias",
                        principalColumn: "BonusCriteriaId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BonusEligibility_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0ce51e75-a671-4e71-8ee3-3f6c792004e5", "1", "Admin", "Admin" },
                    { "ae0c9a98-bd2e-4a14-8200-4729c44390cc", "2", "HR", "HR" },
                    { "c829303d-c5f9-448f-adbb-6e281e54ea9c", "3", "Employee", "Employee" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_BonusDistributions_BonusEligibilityId",
                table: "BonusDistributions",
                column: "BonusEligibilityId");

            migrationBuilder.CreateIndex(
                name: "IX_BonusEligibility_BonusCriteriaId",
                table: "BonusEligibility",
                column: "BonusCriteriaId");

            migrationBuilder.CreateIndex(
                name: "IX_BonusEligibility_EmployeeId",
                table: "BonusEligibility",
                column: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_BonusDistributions_BonusEligibility_BonusEligibilityId",
                table: "BonusDistributions",
                column: "BonusEligibilityId",
                principalTable: "BonusEligibility",
                principalColumn: "BonusEligibilityId");
        }
    }
}
