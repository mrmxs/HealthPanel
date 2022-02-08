namespace HealthPanel.Services.Stats.Dtos
{
    public class DoctorDto : IDto
    {       
        public int Id { get; set; }
        public string Name { get; set; }
        public int HealthFacilityBranchId { get; set; }
    }
}