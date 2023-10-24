using System.Text.Json.Serialization;
using WebApi.Entities;

namespace WebApi.Dto
{
    public class FootballTeamForUpdateDto
    {
        [JsonIgnore] public int TeamId { get; set; }
        public string TeamName { get; set; }
        public string TeamCountry { get; set; }
        public string TeamCountryRegion { get; set; }

    }
}