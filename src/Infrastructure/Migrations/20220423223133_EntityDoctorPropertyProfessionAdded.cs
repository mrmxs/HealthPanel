using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HealthPanel.Infrastructure.Migrations
{
    public partial class EntityDoctorPropertyProfessionAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "profession",
                table: "doctors",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "profession",
                table: "doctors");
        }
    }
}
