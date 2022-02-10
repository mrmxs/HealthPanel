namespace HealthPanel.Core.Entities
{
    public class LabMedTest
    {
        public int Id { get; internal set; }
        public int HealthFacilityBranchId { get; set; }
        public int TestId { get; set; }
        public string CustomName { get; set; }
        public double Min { get; set; }
        public double Max { get; set; }
    }
}