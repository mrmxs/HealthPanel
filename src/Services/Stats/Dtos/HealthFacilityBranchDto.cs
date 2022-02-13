using HealthPanel.Core.Entities;

namespace HealthPanel.Services.Stats.Dtos
{
    public class HealthFacilityBranchDto : IDto
    {
        public int Id { get; set; }
        public int HealthFacilityId { get; set; } 
        public string Name { get; set; }
        public string Address { get; set; }
        public string Type { get; set; }
        

        public HealthFacilityBranchDto() {}

        public HealthFacilityBranchDto(HealthFacilityBranch entity)
        {
            this.Id = entity.Id;
            this.HealthFacilityId = entity.HealthFacilityId;
            this.Name = entity.Name;
            this.Address = entity.Address;
            this.Type = entity.Type.ToString();
        }

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