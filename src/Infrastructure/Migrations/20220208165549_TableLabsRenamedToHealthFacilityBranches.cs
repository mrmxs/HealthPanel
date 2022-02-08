using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace HealthPanel.Infrastructure.Migrations
{
    public partial class TableLabsRenamedToHealthFacilityBranches : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // migrationBuilder.RenameTable("labs", "health_facility_branches");
            migrationBuilder
                .Sql($"ALTER TABLE labs RENAME TO health_facility_branches;");
            migrationBuilder
                .Sql($"ALTER TABLE health_facility_branches RENAME CONSTRAINT pk_labs TO pk_health_facility_branches;");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // migrationBuilder.RenameTable("health_facility_branches", "labs");            
            migrationBuilder
                .Sql($"ALTER TABLE health_facility_branches RENAME TO labs;");
            migrationBuilder
                .Sql($"ALTER TABLE labs RENAME CONSTRAINT pk_health_facility_branches TO pk_labs;");
        }
    }
}
