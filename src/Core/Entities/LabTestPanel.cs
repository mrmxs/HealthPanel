namespace HealthPanel.Core.Entities
{
    /// <summary>
    /// Группа/панель тестов, выпоняемая организацией одномометно
    /// </summary>
    public class LabTestPanel : IEntity
    {
        public int Id { get; internal set; }
        public int HealthFacilityBranchId { get; set; }
        public int TestPanelId { get; set; }
        public string CustomName { get; set; }
        public int[] LabTestIds { get; set; }
    }
}