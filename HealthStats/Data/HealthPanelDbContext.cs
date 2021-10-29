using Microsoft.EntityFrameworkCore;
using HealthPanel.Stats.Models;

namespace HealthPanel.Stats
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