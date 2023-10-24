using WebApi.Dto;
using WebApi.Entities;

namespace WebApi.Contracts
{
    public interface IFootballTeamRepository
    {
        Task<IEnumerable<FootballTeam>> GetTeams();
        Task<IEnumerable<Player>> GetPlayers();
        Task<FootballTeam> GetTeam(int id);
        Task<Player> GetPlayer(int id);
        Task<FootballTeam> CreateTeam(FootballTeamForCreationDto team);
        Task<Player> CreatePlayer(FootballPlayerForCreationDto player);
        Task UpdateTeam(int id, FootballTeamForUpdateDto team);
        Task UpdatePlayer(int id, FootballPlayerForUpdateDto player);
        Task DeletePlayer(int id); 
        Task DeleteTeam(int id);
        Task<FootballTeam> GetTeamByPlayerId(int id);
        //Task<List<FootballTeam>> GetTeamsPlayersMultipleMapping();
    }
}