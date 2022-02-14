using HealthPanel.Core.Entities;

namespace HealthPanel.Services.Stats.Dtos
{
    public class TestPanelDto : IDto
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public TestPanelDto() { }

        public TestPanelDto(TestPanel entity)
        {
            this.Id = entity.Id;
            this.Name = entity.Name;
        }
    }
}