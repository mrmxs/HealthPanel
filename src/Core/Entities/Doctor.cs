namespace HealthPanel.Core.Entities
{
    /// <summary>
    /// Врач медицинского учреждения
    /// Может работать в нескольких учреждениях
    /// </summary>
    public class Doctor : IEntity
    {
        public int Id { get; internal set; }
        public string Name { get; set; }
        public int HealthFacilityBranchId { get; set; }
    }
}