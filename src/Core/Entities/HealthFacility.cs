namespace HealthPanel.Core.Entities
{
    /// <summary>
    /// Медицинское учреждение
    /// Юр. лицо с головным офисом и зарегистрированны назвнием.
    /// Может иметь несколько филиалов в разных сферах услуг и по разным адресам
    /// </summary>    
    public class HealthFacility : IEntity
    {
        public int Id { get; internal set; }
        public string Name { get; set; }   
        public string Address { get; set; } 
    }
}