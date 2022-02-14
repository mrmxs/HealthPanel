using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HealthPanel.Infrastructure.Migrations
{
    public partial class EntityLabTestPanelPropertyTestPanelIdAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "test_panels",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<int[]>(
                name: "lab_test_ids",
                table: "lab_test_panels",
                type: "integer[]",
                nullable: false,
                defaultValue: new int[0],
                oldClrType: typeof(int[]),
                oldType: "integer[]",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "custom_name",
                table: "lab_test_panels",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "test_panel_id",
                table: "lab_test_panels",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "test_panel_id",
                table: "lab_test_panels");

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "test_panels",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<int[]>(
                name: "lab_test_ids",
                table: "lab_test_panels",
                type: "integer[]",
                nullable: true,
                oldClrType: typeof(int[]),
                oldType: "integer[]");

            migrationBuilder.AlterColumn<string>(
                name: "custom_name",
                table: "lab_test_panels",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");
        }
    }
}
