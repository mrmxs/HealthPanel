namespace HealthPanel.Services.Stats.Dtos
{
    public class LabDto : IDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        
        // public static explicit operator LabDto(Lab entity)
        // {
        //     return new LabDto
        //     {
        //         Id = entity.Id,
        //         Name = entity.Name,
        //     };
        // }
        
        // public static explicit operator Lab(LabDto dto)
        // {
        //     return new Lab
        //     {
        //         Name = dto.Name,
        //     };
        // }
    }
 
}