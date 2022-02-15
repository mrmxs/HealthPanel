namespace HealthPanel.Core.Entities
{
    /// <summary>
    /// Группа/панель тестов. Базовая сущность с общепринятым названием
    /// </summary>
    public class TestPanel : IEntity
    {
        public int Id { get; internal set; }
        public string Name { get; set; }
    }
}