using Microsoft.EntityFrameworkCore;
using HealthStats.Models;

namespace HealthStats
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
    }
}