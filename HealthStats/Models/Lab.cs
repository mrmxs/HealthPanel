using Microsoft.EntityFrameworkCore;

namespace HealthStats.Models
{
    [Index(nameof(Name), IsUnique = true)] //todo migration
    public class Lab
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}