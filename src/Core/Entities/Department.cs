namespace HealthPanel.Core.Entities
{
    /// <summary>
    /// Отделение медицинского учреждения
    /// </summary>   
    public class Department : IEntity
    {
        public int Id { get; internal set; }
        public string Name { get; set; }
        public int HealthFacilityBranchId { get; set; }
        public int HeadId { get; set; }

    }
}