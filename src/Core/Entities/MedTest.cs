namespace HealthPanel.Core.Entities
{
    // [Index(nameof(Name), IsUnique = true)] //todo
    public class MedTest
    {
        public int Id { get; internal set; }
        public string Name { get; set; }
        public string Units { get; set; }
    }
}