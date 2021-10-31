namespace HealthPanel.Core.Entities
{
    public class LabMedicalTest
    {
        public int Id { get; internal set; }
        public int LabId { get; set; }
        public int TestId { get; set; }
        public double Min { get; set; }
        public double Max { get; set; }
    }
}