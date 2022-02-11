using HealthPanel.Core.Entities;

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

        public ExaminationDto(
            Examination examinationEntity,
            HealthFacilityBranch branchEntity,
            MedTest medTestEntity)
        {
            this.Id = examinationEntity.Id;
            this.HealthFacilityBranchId = examinationEntity.HealthFacilityBranchId;
            this.HealthFacilityBranchName = branchEntity.Name;
            this.TestId = examinationEntity.TestId;
            this.TestName = medTestEntity.Name;
            this.CustomTestName = examinationEntity.CustomName;
        }
    }
}