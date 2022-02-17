using HealthPanel.Core.Entities;

namespace HealthPanel.Services.Stats.Dtos
{
    public class TestListDto : IDto
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public TestListDto() { }

        public TestListDto(TestList entity)
        {
            this.Id = entity.Id;
            this.Name = entity.Name;
        }
    }
}