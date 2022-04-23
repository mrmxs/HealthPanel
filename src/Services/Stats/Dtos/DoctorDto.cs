using HealthPanel.Core.Entities;

namespace HealthPanel.Services.Stats.Dtos
{
    public class DoctorDto : IDto
    {       
        public int Id { get; set; }
        public string Name { get; set; }
        public string Profession { get; set; }
        public int HealthFacilityBranchId { get; set; }


        public DoctorDto() {}

        public DoctorDto(Doctor doctorEntity)
        {
            this.Id = doctorEntity.Id;
            this.Name = doctorEntity.Name;
            this.HealthFacilityBranchId = 
                doctorEntity.HealthFacilityBranchId;
            this.Profession = doctorEntity.Profession;
        }
    }
}