namespace HealthPanel.Services.Stats.Dtos
{
    public class ExaminationDto : IDto
    {
        public int Id { get; internal set; }
        public int HealthFacilityBranchId { get; set; }  // todo (с типом Hospital/Clinic ?)
        public int TestId { get; set; }
    }
}