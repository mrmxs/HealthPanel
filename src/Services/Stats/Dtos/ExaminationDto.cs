namespace HealthPanel.Services.Stats.Dtos
{
    public class ExaminationDto : IDto
    {
        public int Id { get; internal set; }
        public int HealthFacilityBranchId { get; set; }  // todo (с типом Hospital/Clinic ?)
        public string HealthFacilityBranchName { get; set; }
        public int TestId { get; set; }
        public string TestName { get; set; }
        public string CustomTestName { get; set; }
    }
}