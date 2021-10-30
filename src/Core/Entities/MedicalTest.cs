namespace HealthPanel.Core.Entities
{
    // [Index(nameof(Name), IsUnique = true)] //todo
    public class MedicalTest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Units { get; set; }
    }
}