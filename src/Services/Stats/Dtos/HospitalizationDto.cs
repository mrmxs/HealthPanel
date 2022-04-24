using HealthPanel.Core.Entities;

namespace HealthPanel.Services.Stats.Dtos
{
    public class HospitalizationDto : IDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int HealthFacilityBranchId { get; set; }
        public int Department { get; set; }
        public int PreHplzTListId { get; set; }
        public int DefaultDoctorId { get; set; }

        public HospitalizationDto() { }

        public HospitalizationDto(Hospitalization hsptlztnEntity)
        {
            this.Id = hsptlztnEntity.Id;
            this.Name = hsptlztnEntity.Name;
            this.HealthFacilityBranchId =
                hsptlztnEntity.HealthFacilityBranchId;
            this.Department = hsptlztnEntity.Department;
            this.PreHplzTListId = hsptlztnEntity.PreHplzTListId;
            this.DefaultDoctorId = hsptlztnEntity.DefaultDoctorId;
        }
    }
}