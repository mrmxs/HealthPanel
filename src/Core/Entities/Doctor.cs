namespace HealthPanel.Core.Entities
{
    // [Index(nameof(Name), IsUnique = true)] //todo
    public class Doctor : IEntity
    {
        public int Id { get; internal set; }
        public string Name { get; set; }
        public int HealthFacilityBranchId { get; set; }
    }
}