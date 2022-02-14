using System.ComponentModel.DataAnnotations;
using HealthPanel.Core.Enums;

namespace HealthPanel.Core.Entities
{
    /// <summary>
    /// Филиал медицинского учреждения в конкретной сфере услуг
    /// Может принадлежать типам: Больница, Поликлиника, Лаборатория и другим
    /// </summary>
    public class HealthFacilityBranch : IEntity
    {
        public int Id { get; internal set; }
        [Required]
        public int HealthFacilityId { get; set; } 
        [Required]
        public string Name { get; set; }
        public string Address { get; set; }
        [Required]
        public HealthFacilityBranchType Type { get; set; }
    }
}