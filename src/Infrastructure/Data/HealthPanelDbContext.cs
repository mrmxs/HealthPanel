using HealthPanel.Core.Entities;
using Microsoft.EntityFrameworkCore;
namespace HealthPanel.Infrastructure.Data
{
    public class HealthPanelDbContext : DbContext
    {
        public HealthPanelDbContext(DbContextOptions<HealthPanelDbContext> options)
            : base(options)
        {
        }

        public DbSet<MedTest> Tests { get; set; }
        public DbSet<LabTest> LabTests { get; set; }
        public DbSet<Examination> Examinations { get; set; }
        public DbSet<TestPanel> TestPanels { get; set; }
        public DbSet<LabTestPanel> LabTestPanels { get; set; }
        public DbSet<TestList> TestLists { get; set; }
        public DbSet<TestToTestList> TestsToTestList { get; set; }

        public DbSet<HealthFacility> HealthFacilities { get; set; }
        public DbSet<HealthFacilityBranch> HealthFacilityBranches { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Hospitalization> Hospitalizations { get; set; }

        public DbSet<User> Users { get; set; }
        public DbSet<UserLabTest> UserLabTests { get; set; }
        public DbSet<UserExamination> UserExaminations { get; set; }
        public DbSet<UserHospitalization> UserHospitalizations { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder
                .UseNpgsql("Host=localhost;Database=hpdb;Username=postgres;Password=postgres;CommandTimeout=300")
                .UseSnakeCaseNamingConvention();
    }
}