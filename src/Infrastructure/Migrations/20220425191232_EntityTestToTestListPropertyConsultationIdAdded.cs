using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HealthPanel.Infrastructure.Migrations
{
    public partial class EntityTestToTestListPropertyConsultationIdAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "consultation_id",
                table: "tests_to_test_list",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "consultation_id",
                table: "tests_to_test_list");
        }
    }
}
