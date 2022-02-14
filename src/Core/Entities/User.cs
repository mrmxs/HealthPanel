namespace HealthPanel.Core.Entities
{
    /// <summary>
    /// Пациент. Пользователь приложения
    /// </summary>
    public class User : IEntity
    {
        public int Id { get; internal set; }
        public string Name { get; set; }
    }
}