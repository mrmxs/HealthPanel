namespace HealthPanel.Core.Entities
{
    /// <summary>
    /// Шаблон госпитализации в медицинском учреждении
    /// </summary>
    public class Hospitalization : IEntity
    {
        public int Id { get; internal set; }
        public string Name { get; set; }
        public int HealthFacilityBranchId { get; set; }
        public int Department { get; set; }
        public int PreHplzTListId { get; set; }
        public int DefaultDoctorId { get; set; }
    }
}