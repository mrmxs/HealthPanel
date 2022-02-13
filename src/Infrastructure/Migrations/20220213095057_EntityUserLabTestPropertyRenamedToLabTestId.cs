using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HealthPanel.Infrastructure.Migrations
{
    public partial class EntityUserLabTestPropertyRenamedToLabTestId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "lab_med_test_id",
                table: "user_lab_tests",
                newName: "lab_test_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "lab_test_id",
                table: "user_lab_tests",
                newName: "lab_med_test_id");
        }
    }
}
