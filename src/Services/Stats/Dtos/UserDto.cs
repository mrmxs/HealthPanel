using HealthPanel.Core.Entities;

namespace HealthPanel.Services.Stats.Dtos
{
    public class UserDto : IDto
    {
        public int Id { get; set; }
        public string Name { get; set; } 

        public UserDto() {}

        public UserDto(User userEntity)
        {
            this.Id = userEntity.Id;
            this.Name = userEntity.Name;        
        }
    }
}