using Microsoft.EntityFrameworkCore;
using HealthPanel.Core.Entities;

namespace HealthPanel.Infrastructure.Data
{
    public class HealthPanelDbContext : DbContext
    {
        public HealthPanelDbContext(DbContextOptions<HealthPanelDbContext> options)
            : base(options)
        {
        }

        public DbSet<MedicalTest> Tests { get; set; }
        public DbSet<LabMedicalTest> LabTests { get; set; }
        public DbSet<Lab> Labs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder
                .UseNpgsql("Host=localhost;Database=hpdb;Username=postgres;Password=postgres")
                .UseSnakeCaseNamingConvention();
    }
}