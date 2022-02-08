using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HealthPanel.Infrastructure.Migrations
{
    public partial class EntityHealthFacilityBranchPropertiesAddedAndLabMedicalTestPropertyEdited : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "lab_id",
                table: "lab_tests",
                newName: "health_facility_branch_id");

            migrationBuilder.AddColumn<string>(
                name: "address",
                table: "health_facility_branches",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "health_facility_id",
                table: "health_facility_branches",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "type",
                table: "health_facility_branches",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "address",
                table: "health_facility_branches");

            migrationBuilder.DropColumn(
                name: "health_facility_id",
                table: "health_facility_branches");

            migrationBuilder.DropColumn(
                name: "type",
                table: "health_facility_branches");

            migrationBuilder.RenameColumn(
                name: "health_facility_branch_id",
                table: "lab_tests",
                newName: "lab_id");
        }
    }
}
