using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace HealthPanel.Infrastructure.Migrations
{
    public partial class EntitiesTestListAndTestToTestListCreated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "test_lists",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_test_lists", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "tests_to_test_list",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    test_list_id = table.Column<int>(type: "integer", nullable: false),
                    med_test_id = table.Column<int>(type: "integer", nullable: false),
                    lab_test_id = table.Column<int>(type: "integer", nullable: false),
                    examination_id = table.Column<int>(type: "integer", nullable: false),
                    test_panel_id = table.Column<int>(type: "integer", nullable: false),
                    lab_test_panel_id = table.Column<int>(type: "integer", nullable: false),
                    index = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_tests_to_test_list", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "test_lists");

            migrationBuilder.DropTable(
                name: "tests_to_test_list");
        }
    }
}
