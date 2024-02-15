using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HRMSDevelopment.Model.Migrations
{
    /// <inheritdoc />
    public partial class BonusManagementModuleChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BonusDistributions_BonusEligibilities_BonusEligibilityId",
                table: "BonusDistributions");

            migrationBuilder.DropForeignKey(
                name: "FK_BonusEligibilities_BonusCriterias_BonusCriteriaId",
                table: "BonusEligibilities");

            migrationBuilder.DropForeignKey(
                name: "FK_BonusEligibilities_Employees_EmployeeId",
                table: "BonusEligibilities");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BonusEligibilities",
                table: "BonusEligibilities");

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

            migrationBuilder.RenameTable(
                name: "BonusEligibilities",
                newName: "BonusEligibility");

            migrationBuilder.RenameIndex(
                name: "IX_BonusEligibilities_EmployeeId",
                table: "BonusEligibility",
                newName: "IX_BonusEligibility_EmployeeId");

            migrationBuilder.RenameIndex(
                name: "IX_BonusEligibilities_BonusCriteriaId",
                table: "BonusEligibility",
                newName: "IX_BonusEligibility_BonusCriteriaId");

            migrationBuilder.AlterColumn<int>(
                name: "BonusEligibilityId",
                table: "BonusDistributions",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "BonusCriteriaId",
                table: "BonusDistributions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "MaximumBonusAmount",
                table: "BonusCriterias",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "MinimumBonusAmount",
                table: "BonusCriterias",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddPrimaryKey(
                name: "PK_BonusEligibility",
                table: "BonusEligibility",
                column: "BonusEligibilityId");

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
                name: "IX_BonusDistributions_BonusCriteriaId",
                table: "BonusDistributions",
                column: "BonusCriteriaId");

            migrationBuilder.AddForeignKey(
                name: "FK_BonusDistributions_BonusCriterias_BonusCriteriaId",
                table: "BonusDistributions",
                column: "BonusCriteriaId",
                principalTable: "BonusCriterias",
                principalColumn: "BonusCriteriaId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BonusDistributions_BonusEligibility_BonusEligibilityId",
                table: "BonusDistributions",
                column: "BonusEligibilityId",
                principalTable: "BonusEligibility",
                principalColumn: "BonusEligibilityId");

            migrationBuilder.AddForeignKey(
                name: "FK_BonusEligibility_BonusCriterias_BonusCriteriaId",
                table: "BonusEligibility",
                column: "BonusCriteriaId",
                principalTable: "BonusCriterias",
                principalColumn: "BonusCriteriaId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BonusEligibility_Employees_EmployeeId",
                table: "BonusEligibility",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "EmployeeId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BonusDistributions_BonusCriterias_BonusCriteriaId",
                table: "BonusDistributions");

            migrationBuilder.DropForeignKey(
                name: "FK_BonusDistributions_BonusEligibility_BonusEligibilityId",
                table: "BonusDistributions");

            migrationBuilder.DropForeignKey(
                name: "FK_BonusEligibility_BonusCriterias_BonusCriteriaId",
                table: "BonusEligibility");

            migrationBuilder.DropForeignKey(
                name: "FK_BonusEligibility_Employees_EmployeeId",
                table: "BonusEligibility");

            migrationBuilder.DropIndex(
                name: "IX_BonusDistributions_BonusCriteriaId",
                table: "BonusDistributions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BonusEligibility",
                table: "BonusEligibility");

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
                name: "BonusCriteriaId",
                table: "BonusDistributions");

            migrationBuilder.DropColumn(
                name: "MaximumBonusAmount",
                table: "BonusCriterias");

            migrationBuilder.DropColumn(
                name: "MinimumBonusAmount",
                table: "BonusCriterias");

            migrationBuilder.RenameTable(
                name: "BonusEligibility",
                newName: "BonusEligibilities");

            migrationBuilder.RenameIndex(
                name: "IX_BonusEligibility_EmployeeId",
                table: "BonusEligibilities",
                newName: "IX_BonusEligibilities_EmployeeId");

            migrationBuilder.RenameIndex(
                name: "IX_BonusEligibility_BonusCriteriaId",
                table: "BonusEligibilities",
                newName: "IX_BonusEligibilities_BonusCriteriaId");

            migrationBuilder.AlterColumn<int>(
                name: "BonusEligibilityId",
                table: "BonusDistributions",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_BonusEligibilities",
                table: "BonusEligibilities",
                column: "BonusEligibilityId");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "91a57178-9f2b-4ae3-889f-0b0d0f1c16b2", "2", "HR", "HR" },
                    { "c209d745-dcf7-415c-a449-0d759e33f1b2", "1", "Admin", "Admin" },
                    { "e3af3570-f64a-4592-878b-69e00484f253", "3", "Employee", "Employee" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_BonusDistributions_BonusEligibilities_BonusEligibilityId",
                table: "BonusDistributions",
                column: "BonusEligibilityId",
                principalTable: "BonusEligibilities",
                principalColumn: "BonusEligibilityId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BonusEligibilities_BonusCriterias_BonusCriteriaId",
                table: "BonusEligibilities",
                column: "BonusCriteriaId",
                principalTable: "BonusCriterias",
                principalColumn: "BonusCriteriaId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BonusEligibilities_Employees_EmployeeId",
                table: "BonusEligibilities",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "EmployeeId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
