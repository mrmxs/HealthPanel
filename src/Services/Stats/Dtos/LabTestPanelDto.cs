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

        public LabTestPanelDto() { }

        public LabTestPanelDto(
            LabTestPanel labTestPanelEntity,
            HealthFacilityBranch branchEntity,
            TestPanel testPanelEntity)
        {
            this.Id = labTestPanelEntity.Id;
            this.HealthFacilityBranchId = labTestPanelEntity.HealthFacilityBranchId;
            this.HealthFacilityBranchName = branchEntity.Name;
            this.TestPanelName = testPanelEntity.Name;
            this.CustomTestPanelName = labTestPanelEntity.CustomName;
            this.LabTestIds = labTestPanelEntity.LabTestIds;
        }
    }
}