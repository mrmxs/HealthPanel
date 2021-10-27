using Microsoft.EntityFrameworkCore;
using HealthStats.Models;

namespace HealthStats
{
    public class HealthContext : DbContext
    {
        public HealthContext(DbContextOptions<HealthContext> options)
            : base(options)
        {
        }

        public DbSet<BodyType> BodyTypes { get; set; }
    }
}