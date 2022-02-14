namespace HealthPanel.Core.Entities
{
    /// <summary>
    /// Базовая сущность для любых медицинских тестов/исследовний
    /// Хранит общепринятое названием и единицы измерения
    /// </summary>
    public class MedTest : IEntity
    {
        public int Id { get; internal set; }
        public string Name { get; set; }
        public string Units { get; set; }
    }
}