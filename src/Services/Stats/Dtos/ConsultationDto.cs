using HealthPanel.Core.Entities;

namespace HealthPanel.Services.Stats.Dtos
{
    public class ConsultationDto : IDto
    {
        public int Id { get; set; }
        public int HealthFacilityBranchId { get; set; }
        public int TestId { get; set; }
        public string CustomName { get; set; }

        public ConsultationDto() { }

        public ConsultationDto(Consultation entity)
        {
            this.Id = entity.Id;
            this.HealthFacilityBranchId =
                entity.HealthFacilityBranchId;
            this.TestId = entity.TestId;
            this.CustomName = entity.CustomName;
        }
    }
}