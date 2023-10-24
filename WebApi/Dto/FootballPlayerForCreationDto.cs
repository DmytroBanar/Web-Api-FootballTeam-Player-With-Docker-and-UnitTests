using WebApi.Entities;

namespace WebApi.Dto
{
    public class FootballPlayerForCreationDto
    {
        public int PlayerId { get; set; }
        public string PlayerName { get; set; }
        public string PlayerSurname { get; set; }
        public int PlayerAge { get; set; }
        public string TeamName { get; set; }
        public string PlayerCountry { get; set; }
        public string PlayerPosition { get; set; }
        public int PlayerCostInMillions { get; set; }
        public int TeamNameId { get; set; }
    }
}
