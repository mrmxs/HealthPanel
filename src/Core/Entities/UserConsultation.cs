using HealthPanel.Core.Enums;

namespace HealthPanel.Core.Entities
{
    /// <summary>
    /// Проведенная или запланированная у пациента
    /// консультация врача
    /// </summary>
    public class UserConsultation : IEntity
    {
        public int Id { get; internal set; }
        public int ConsultationId { get; set; }
        public int DoctorId { get; set; }
        public DateTime Date { get; set; }
        public int UserId { get; set; }
        public string Value { get; set; }
        public TestStatus Status { get; set; }
    }
}