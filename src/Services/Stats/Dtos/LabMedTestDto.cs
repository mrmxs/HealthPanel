using System.ComponentModel.DataAnnotations;
namespace HealthPanel.Services.Stats.Dtos
{
    public class LabMedTestDto : IDto
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
        
        // public int Id { get; internal set; }
        // public int HealthFacilityBranchId { get; set; }
        // public int TestId { get; set; }
        // public double Min { get; set; }
        // public double Max { get; set; }

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