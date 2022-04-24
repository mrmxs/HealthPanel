using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HealthPanel.Infrastructure.Migrations
{
    public partial class EntitiesHospitalizationAndTestToTestListEdited : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "pk_hospitalization",
                table: "hospitalization");

            migrationBuilder.RenameTable(
                name: "hospitalization",
                newName: "hospitalizations");

            migrationBuilder.AddPrimaryKey(
                name: "pk_hospitalizations",
                table: "hospitalizations",
                column: "id");

            migrationBuilder.AddColumn<TimeSpan>(
                name: "ttl",
                table: "tests_to_test_list",
                type: "interval",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "pk_hospitalizations",
                table: "hospitalizations");

            migrationBuilder.RenameTable(
                name: "hospitalizations",
                newName: "hospitalization");

            migrationBuilder.AddPrimaryKey(
                name: "pk_hospitalization",
                table: "hospitalization",
                column: "id");

            migrationBuilder.DropColumn(
                name: "ttl",
                table: "tests_to_test_list");
        }
    }
}
