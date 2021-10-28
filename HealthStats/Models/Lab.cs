using System.ComponentModel.DataAnnotations;

namespace HealthStats.Models
{
    public class Lab
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}