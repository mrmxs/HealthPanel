using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace HealthPanel.Infrastructure.Data
{
    public class HealthPanelContextFactory : IDesignTimeDbContextFactory<HealthPanelDbContext>
    {
        public HealthPanelDbContext CreateDbContext(string[] args)
        {
            return new HealthPanelDbContext(
                new DbContextOptionsBuilder<HealthPanelDbContext>().Options);
        }        
    }
}