namespace HealthPanel.Core.Entities
{
    /// <summary>
    /// Состоявшаяся или запланированная 
    /// госпитализация полльзователя
    /// </summary>
    public class UserHospitalization : IEntity
    {
        public int Id { get; internal set; }
        public int HospitalizationId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int DoctorId { get; set; }
        public string DischargeSummary { get; set; }
        public string Notes { get; set; }
    }
}