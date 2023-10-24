using WebApi.Dto;
using WebApi.Entities;

namespace WebApi.Contracts
{
    public interface IFootballTeamRepository
    {
        Task<IEnumerable<FootballTeam>> GetTeams();
        Task<FootballTeam> GetTeam(int id);
        Task<FootballTeam> CreateTeam(FootballTeamForCreationDto team);
        Task UpdateTeam(int id, FootballTeamForUpdateDto team);
        Task DeleteTeam(int id); 
        Task<FootballTeam> GetTeamByPlayerId(int id);
        //Task<List<FootballTeam>> GetTeamsPlayersMultipleMapping();
    }
}