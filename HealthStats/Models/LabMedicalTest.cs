using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HealthStats.Models
{
    public class LabMedicalTest
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Lab")]
        public int LabId { get; set; }
        [ForeignKey("FK_MedicalTest")]
        public int TestId { get; set; }
        public double Min { get; set; }
        public double Max { get; set; }
    }
}