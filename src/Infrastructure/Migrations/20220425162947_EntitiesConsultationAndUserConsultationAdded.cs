using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace HealthPanel.Infrastructure.Migrations
{
    public partial class EntitiesConsultationAndUserConsultationAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "consultations",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    health_facility_branch_id = table.Column<int>(type: "integer", nullable: false),
                    test_id = table.Column<int>(type: "integer", nullable: false),
                    custom_name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_consultations", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "user_consultations",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    consultation_id = table.Column<int>(type: "integer", nullable: false),
                    doctor_id = table.Column<int>(type: "integer", nullable: false),
                    date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    user_id = table.Column<int>(type: "integer", nullable: false),
                    value = table.Column<string>(type: "text", nullable: false),
                    status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user_consultations", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "consultations");

            migrationBuilder.DropTable(
                name: "user_consultations");
        }
    }
}
