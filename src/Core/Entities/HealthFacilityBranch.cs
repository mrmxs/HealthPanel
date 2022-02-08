namespace HealthPanel.Core.Entities
{
    // [Index(nameof(Name), IsUnique = true)] //todo
    public class HealthFacilityBranch
    {
        public int Id { get; internal set; }
        public string Name { get; set; }
    }
}