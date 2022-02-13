using System.ComponentModel.DataAnnotations;
using HealthPanel.Core.Enums;

namespace HealthPanel.Core.Entities
{
    public class UserLabTest : IEntity
    {
        public int Id { get; internal set; }        
        [Required]
        public int LabMedTestId { get; set; } 
        [Required] 
        public int UserId { get; set; }
        public DateTime Date { get; set; }
        public int Value { get; set; }
        public TestStatus Status { get; set; }
    }
}