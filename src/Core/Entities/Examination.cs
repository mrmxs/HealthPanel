namespace HealthPanel.Core.Entities
{
    public class Examination
    {
        public int Id { get; internal set; }
        public int HealthFacilityBranchId { get; set; }  // todo (с типом Hospital/Clinic ?)
        public int TestId { get; set; }
        public string CustomName { get; set; }
    }
}