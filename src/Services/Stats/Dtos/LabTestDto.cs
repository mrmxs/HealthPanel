using System.ComponentModel.DataAnnotations;
using HealthPanel.Core.Entities;

namespace HealthPanel.Services.Stats.Dtos
{
    public class LabTestDto : IDto
    {
        public int Id { get; internal set; }
        public int HealthFacilityBranchId { get; set; }
        public string HealthFacilityBranchName { get; set; }
        public int TestId { get; set; }
        public string TestName { get; set; }
        public string CustomTestName { get; set; }
        public string Units { get; set; }
        public double Min { get; set; }
        public double Max { get; set; }
        
        public LabTestDto () {}            
        
        public LabTestDto (            
            LabTest labTestEntity,
            HealthFacilityBranch branchEntity,
            MedTest medTestEntity)
        {
            this.Id = labTestEntity.Id;
            this.HealthFacilityBranchId = labTestEntity.HealthFacilityBranchId;
            this.HealthFacilityBranchName = branchEntity.Name;
            this.TestId = labTestEntity.TestId;
            this.TestName = medTestEntity.Name;
            this.CustomTestName = labTestEntity.CustomName;
            this.Units = medTestEntity.Units;
            this.Min = labTestEntity.Min;
            this.Max = labTestEntity.Max;
        }

        // public static explicit operator LabMedicalTestDto(LabMedicalTest entity)
        // {
        //     return new LabMedicalTestDto
        //     {
        //         Id = entity.Id,
        //         Name = entity.Name,
        //         Units = entity.Units,
        //     };
        // }
        
        // public static explicit operator LabMedicalTest(LabMedicalTestDto dto)
        // {
        //     return new LabMedicalTest
        //     {
        //         Name = dto.Name,
        //         Units = dto.Units,
        //     };
        // }
    }
}