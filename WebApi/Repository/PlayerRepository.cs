using Dapper;
using System.Data;
using Npgsql;
using WebApi.Contracts;
using WebApi.Dto;
using WebApi.Entities;

namespace WebApi.Repository
{
    public class PlayerRepository : IPlayerRepository
    {
        private readonly string _connectionString;

        public PlayerRepository(string connectionString = "Host=localhost;Port=5432;Database=Football;Username=postgres;Password=29022004bd;")
        {
            _connectionString = connectionString;
        }

        public IDbConnection CreateConnection()
        {
            return new NpgsqlConnection(_connectionString);
        }

        public async Task<IEnumerable<Player>> GetPlayers()
        {
            using (var connection = CreateConnection())
            {
                connection.Open();
                var query = "SELECT PlayerId, PlayerName, PlayerSurname, PlayerAge, TeamName, PlayerCountry, PlayerPosition, PlayerCostInMillions, TeamNameId FROM Player";
                return await connection.QueryAsync<Player>(query);
            }
        }

        public async Task<Player> GetPlayer(int id)
        {
            using (var connection = CreateConnection())
            {
                connection.Open();
                var query = "SELECT * FROM Player WHERE PlayerId = @PlayerId";
                return await connection.QueryFirstOrDefaultAsync<Player>(query, new { PlayerId = id });
            }
        }

        public async Task<Player> CreatePlayer(FootballPlayerForCreationDto player)
        {
            using (var connection = CreateConnection())
            {
                connection.Open();
                var query = "INSERT INTO Player (PlayerName, PlayerSurname, PlayerAge, TeamName, PlayerCountry, PlayerPosition, PlayerCostInMillions, TeamNameId) " +
                            "VALUES (@PlayerName, @PlayerSurname, @PlayerAge, @TeamName, @PlayerCountry, @PlayerPosition, @PlayerCostInMillions, @TeamNameId) RETURNING PlayerId";
                return await connection.QueryFirstOrDefaultAsync<Player>(query, player);
            }
        }

        public async Task UpdatePlayer(int id, FootballPlayerForUpdateDto player)
        {
            using (var connection = CreateConnection())
            {
                connection.Open();
                var query = "UPDATE Player SET PlayerName = @PlayerName, PlayerAge = @PlayerAge, TeamName = @TeamName, PlayerCountry = @PlayerCountry, PlayerPosition = @PlayerPosition, PlayerCostInMillions = @PlayerCostInMillions, TeamNameId = @TeamNameId " +
            "WHERE PlayerId = @PlayerId";

                await connection.ExecuteAsync(query, new { PlayerId = id, player.PlayerName, player.PlayerSurname, player.PlayerAge, player.TeamName, player.PlayerCountry, player.PlayerPosition, player.PlayerCostInMillions, player.TeamNameId });
            }
        }

        public async Task DeletePlayer(int id)
        {
            using (var connection = CreateConnection())
            {
                connection.Open();
                var query = "DELETE FROM Player WHERE PlayerId = @PlayerId";
                await connection.ExecuteAsync(query, new { PlayerId = id });
            }
        }




        /*public async Task<List<FootballTeam>> GetTeamsPlayersMultipleMapping()
        {
            using (var connection = CreateConnection())
            {
                connection.Open();
                var query = "SELECT t.TeamId, t.TeamName, t.TeamCountry, t.TeamCountryRegion, t.TeamNameId" +
                            "p.PlayerId, p.PlayerName, p.PlayerSurname, p.PlayerAge, p.FootballTeam, " +
                            "p.PlayerCountry, p.PlayerPosition, p.PlayerCostInMillions, p.TeamNameId " +
                            "FROM FootballTeam t " +
                            "LEFT JOIN Players p ON t.TeamId = p.FootballTeam";

                var teamDict = new Dictionary<int, FootballTeam>();
                var teams = new List<FootballTeam>();

                using (var multi = connection.QueryMultiple(query))
                {
                    var results = multi.Read<FootballTeam, Player, FootballTeam>(
                        (team, player) =>
                        {
                            if (!teamDict.TryGetValue(team.TeamId, out var currentTeam))
                            {
                                currentTeam = team;
                                currentTeam.Players = new List<Player>();
                                teamDict.Add(currentTeam.TeamId, currentTeam);
                                teams.Add(currentTeam);
                            }
                            currentTeam.Players.Add(player);
                            return currentTeam;
                        },
                        splitOn: "PlayerId"
                    );

                    // Забезпечте унікальні команди
                    return teams.Distinct().ToList();
                }
            }
        }*/



    }
}
