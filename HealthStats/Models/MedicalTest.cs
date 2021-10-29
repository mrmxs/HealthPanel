using Microsoft.EntityFrameworkCore;

namespace HealthPanel.Stats.Models
{
    [Index(nameof(Name), IsUnique = true)] //todo migration
    public class MedicalTest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Units { get; set; }
    }
}