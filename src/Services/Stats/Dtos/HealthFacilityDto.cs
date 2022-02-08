namespace HealthPanel.Services.Stats.Dtos
{
    public class HealthFacilityDto: IDto
    {
        public int Id { get; set; }
        public string Name { get; set; }   
        public string Address { get; set; }   
    }
}