using System.ComponentModel.DataAnnotations;
using HealthPanel.Core.Enums;

namespace HealthPanel.Core.Entities
{
    /// <summary>
    /// Лабораторное исследование, проведенное (или запланированное) пользователем
    /// в медицинском учреждении, в кокретную дату-время
    /// Содержит результат исследования
    /// </summary>
    public class UserLabTest : IEntity
    {
        public int Id { get; internal set; }        
        [Required]
        public int LabTestId { get; set; } 
        [Required] 
        public int UserId { get; set; }
        public DateTime Date { get; set; }
        public double Value { get; set; }
        public TestStatus Status { get; set; }
    }
}