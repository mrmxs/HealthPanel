using HealthPanel.Core.Entities;

namespace HealthPanel.Services.Stats.Dtos
{
    public class MedTestDto : IDto
    {
        public int Id { get; internal set; }
        public string Name { get; set; }
        public string Units { get; set; }
    
        public MedTestDto() {}

        public MedTestDto(MedTest testEntity)
        {
            this.Id = testEntity.Id;
            this.Name = testEntity.Name;
            this.Units = testEntity.Units;
        }

    // public static explicit operator MedicalTestDto(MedicalTest entity)
        // {
        //     return new MedicalTestDto
        //     {
        //         Id = entity.Id,
        //         Name = entity.Name,
        //         Units = entity.Units,
        //     };
        // }
        
        // public static explicit operator MedicalTest(MedicalTestDto dto)
        // {
        //     return new MedicalTest
        //     {
        //         Name = dto.Name,
        //         Units = dto.Units,
        //     };
        // }
    }
}