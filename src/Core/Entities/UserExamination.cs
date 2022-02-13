using System.ComponentModel.DataAnnotations;
using HealthPanel.Core.Enums;

namespace HealthPanel.Core.Entities
{
    public class UserExamination : IEntity
    {
        public int Id { get; internal set; }        
        [Required]
        public int ExaminationId { get; set; } 
        [Required] 
        public int UserId { get; set; }
        public int DoctorId { get; set; }
        public DateTime Date { get; set; }
        public string Value { get; set; }
        public TestStatus Status { get; set; }        
    }
}