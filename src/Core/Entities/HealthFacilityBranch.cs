using System.ComponentModel.DataAnnotations;
using HealthPanel.Core.Enums;

namespace HealthPanel.Core.Entities
{
    // [Index(nameof(Name), IsUnique = true)] //todo
    public class HealthFacilityBranch
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