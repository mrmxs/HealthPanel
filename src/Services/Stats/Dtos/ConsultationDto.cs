using HealthPanel.Core.Entities;

namespace HealthPanel.Services.Stats.Dtos
{
    public class ConsultationDto : IDto
    {
        public int Id { get; set; }
        public int HealthFacilityBranchId { get; set; }
        public string HealthFacilityBranchName { get; set; }
        public int TestId { get; set; }
        public string TestName { get; set; }
        public string CustomName { get; set; }

        public ConsultationDto() { }

        public ConsultationDto(
            Consultation consultatioEntity,
            HealthFacilityBranch branchEntity,
            MedTest medTestEntity)
        {
            this.Id = consultatioEntity.Id;
            this.HealthFacilityBranchId = consultatioEntity.HealthFacilityBranchId;
            this.HealthFacilityBranchName = branchEntity.Name;
            this.TestId = consultatioEntity.TestId;
            this.TestName = medTestEntity.Name;
            this.CustomName = consultatioEntity.CustomName;
        }
    }
}