using HealthPanel.Core.Entities;

namespace HealthPanel.Services.Stats.Dtos
{
    public class DepartmentDto : IDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int HealthFacilityBranchId { get; set; }
        public int HeadId { get; set; }

        public DepartmentDto() { }

        public DepartmentDto(Department departmentEntity)
        {
            this.Id = departmentEntity.Id;
            this.Name = departmentEntity.Name;
            this.HealthFacilityBranchId =
                departmentEntity.HealthFacilityBranchId;
            this.HeadId = departmentEntity.HeadId;
        }
    }
}