using System.ComponentModel.DataAnnotations;

namespace HealthPanel.Core.Entities
{
    /// <summary>
    /// Сущность для привязки теста/исследования/панели к списку тестов
    /// </summary>
    public class TestToTestList : IEntity
    {
        public int Id { get; internal set; }
        [Required]
        public int TestListId { get; set; }
        public int MedTestId { get; set; } // EX: список для госпитализации
        public int LabTestId { get; set; }
        public int ExaminationId { get; set; }
        public int TestPanelId { get; set; }
        public int LabTestPanelId { get; set; }
        public int Index { get; set; }
    }
}