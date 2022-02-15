using System.Collections.Generic;
using System.Linq;
using HealthPanel.Core.Entities;

namespace HealthPanel.Services.Stats.Dtos
{
    public class LabTestPanelDto : IDto
    {
        public int Id { get; set; }
        public int HealthFacilityBranchId { get; set; }
        public string HealthFacilityBranchName { get; set; }
        public int TestPanelId { get; set; }
        public string TestPanelName { get; set; }
        public string CustomTestPanelName { get; set; }
        public int[] LabTestIds { get; set; }

        public LabTestDto[] Tests { get; set; }

        public LabTestPanelDto() { }

        public LabTestPanelDto(
            LabTestPanel labTestPanelEntity,
            HealthFacilityBranch branchEntity,
            TestPanel testPanelEntity,
            IEnumerable<LabTest> labTestEntities,
            IEnumerable<MedTest> medTestEntities)
        {
            this.Id = labTestPanelEntity.Id;
            this.HealthFacilityBranchId = labTestPanelEntity.HealthFacilityBranchId;
            this.HealthFacilityBranchName = branchEntity.Name;
            this.TestPanelName = testPanelEntity.Name;
            this.CustomTestPanelName = labTestPanelEntity.CustomName;
            this.LabTestIds = labTestPanelEntity.LabTestIds;

            this.Tests = labTestPanelEntity.LabTestIds
                .Select(p =>
                {
                    var labTestEntity = labTestEntities
                        .First(t => t.Id == p);
                    var medTestEntity = medTestEntities
                        .First(t => t.Id == labTestEntity.TestId);

                    return new LabTestDto(
                        labTestEntity, branchEntity, medTestEntity);
                }).ToArray();
        }
    }
}