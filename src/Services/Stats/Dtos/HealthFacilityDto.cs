using HealthPanel.Core.Entities;

namespace HealthPanel.Services.Stats.Dtos
{
    public class HealthFacilityDto: IDto
    {
        public int Id { get; set; }
        public string Name { get; set; }   
        public string Address { get; set; }  

        public HealthFacilityDto() {}
        
        public HealthFacilityDto(HealthFacility entity)
        {
            this.Id = entity.Id;
            this.Name = entity.Name;
            this.Address = entity.Address;
        } 
    }
}