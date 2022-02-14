namespace HealthPanel.Core.Entities
{
    /// <summary>
    /// Инструментальное исследовние, проводимое врачом в организации
    /// </summary>
    public class Examination : IEntity
    {
        public int Id { get; internal set; }
        public int HealthFacilityBranchId { get; set; }
        public int TestId { get; set; }
        public string CustomName { get; set; }
    }
}