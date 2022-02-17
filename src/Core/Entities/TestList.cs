using System.ComponentModel.DataAnnotations;

namespace HealthPanel.Core.Entities
{
    /// <summary>
    /// Список тестов
    /// Для объединения тестов/исследовний/панелей тестов
    /// </summary>
    public class TestList : IEntity
    {
        public int Id { get; internal set; }
        [Required]
        public string Name { get; set; }
    }
}