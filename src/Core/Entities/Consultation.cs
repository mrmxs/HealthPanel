namespace HealthPanel.Core.Entities
{
    /// <summary>
    /// Консультация врача в медицинской организации
    /// </summary>
    public class Consultation : IEntity
    {
        public int Id { get; internal set; }
        public int HealthFacilityBranchId { get; set; }
        public int TestId { get; set; }
        public string CustomName { get; set; }
    }
}