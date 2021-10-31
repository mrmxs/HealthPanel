namespace HealthPanel.Services.Stats.Dtos
{
    public class LabMedicalTestDto : IDto
    {
        public int Id { get; internal set; }
        public string LabName { get; set; }
        public string TestTitle { get; set; }
        public double Min { get; set; }
        public double Max { get; set; }
        
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