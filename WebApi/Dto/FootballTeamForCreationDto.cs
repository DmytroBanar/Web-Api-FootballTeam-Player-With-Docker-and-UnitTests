using WebApi.Entities;

namespace WebApi.Dto
{
    public class FootballTeamForCreationDto
    {
        public int TeamId { get; set; }
        public string TeamName { get; set; }
        public string TeamCountry { get; set; }
        public string TeamCountryRegion { get; set; }
    }
}
