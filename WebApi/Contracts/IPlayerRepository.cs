using WebApi.Dto;
using WebApi.Entities;

namespace WebApi.Contracts
{
    public interface IPlayerRepository
    {
        Task<IEnumerable<Player>> GetPlayers();
        Task<Player> GetPlayer(int id);
        Task<Player> CreatePlayer(FootballPlayerForCreationDto player);
        Task UpdatePlayer(int id, FootballPlayerForUpdateDto player);
        Task DeletePlayer(int id);
    }
}