namespace HealthPanel.Core.Entities
{
    // [Index(nameof(Name), IsUnique = true)] //todo
    public class Lab
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}