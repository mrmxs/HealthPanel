using System.ComponentModel.DataAnnotations;

namespace HealthStats.Models
{
    public class MedicalTest
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Metric { get; set; }
    }
}