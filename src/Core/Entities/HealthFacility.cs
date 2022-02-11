namespace HealthPanel.Core.Entities
{
    // [Index(nameof(Name), IsUnique = true)] //todo
    public class HealthFacility : IEntity
    {
        public int Id { get; internal set; }
        public string Name { get; set; }   
        public string Address { get; set; } 
    }
}