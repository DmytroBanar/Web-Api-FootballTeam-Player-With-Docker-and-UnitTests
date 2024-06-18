using WebApi.Entities;
using WebApi.Repository;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Dapper;
using WebApi.Dto;

namespace FootballReviewApp.Tests.Repositories
{
    public class FootballTeamRepositoryTests
    {
        private readonly IConfiguration _configuration;

        public FootballTeamRepositoryTests()
        {
            var configurationBuilder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables();
            _configuration = configurationBuilder.Build();
        }

        private FootballTeamRepository CreateRepository()
        {
            var connectionString = _configuration.GetConnectionString("SqlConnection");
            return new FootballTeamRepository(connectionString);
        }


        [Fact]
        public async Task FootballTeamRepository_GetTeam_ReturnsTeam()
        {
            // Arrange
            var repository = CreateRepository();
            var team = new FootballTeam
            {
                TeamName = "Liverpool",
                TeamCountry = "England",
                TeamCountryRegion = "Liverpool"
            };

            // Act
            var createdTeam = await repository.CreateTeam(new FootballTeamForCreationDto
            {
                TeamName = team.TeamName,
                TeamCountry = team.TeamCountry,
                TeamCountryRegion = team.TeamCountryRegion
            });
            var result = await repository.GetTeam(createdTeam.TeamId);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<FootballTeam>();
            result.TeamName.Should().Be(team.TeamName);
            result.TeamCountry.Should().Be(team.TeamCountry);
            result.TeamCountryRegion.Should().Be(team.TeamCountryRegion);

            // Clean up
            await repository.DeleteTeam(createdTeam.TeamId);
        }

        [Fact]
        public async Task FootballTeamRepository_GetTeamByPlayerId_ReturnsTeam()
        {
            // Arrange
            var repository = CreateRepository();
            var team = new FootballTeam
            {
                TeamName = "Liverpool",
                TeamCountry = "England",
                TeamCountryRegion = "Liverpool"
            };

            // Create team
            var createdTeam = await repository.CreateTeam(new FootballTeamForCreationDto
            {
                TeamName = team.TeamName,
                TeamCountry = team.TeamCountry,
                TeamCountryRegion = team.TeamCountryRegion
            });

            var player = new Player
            {
                PlayerName = "Darwin",
                PlayerSurname = "Nunez",
                PlayerAge = 24,
                TeamName = team.TeamName,
                PlayerCountry = "Uruguay",
                PlayerPosition = "ST",
                PlayerCostInMillions = 70,
                TeamNameId = createdTeam.TeamId
            };

            using (var connection = repository.CreateConnection())
            {
                var query = "INSERT INTO Player (PlayerName, PlayerSurname, PlayerAge, TeamName, PlayerCountry, PlayerPosition, PlayerCostInMillions, TeamNameId) " +
                            "VALUES (@PlayerName, @PlayerSurname, @PlayerAge, @TeamName, @PlayerCountry, @PlayerPosition, @PlayerCostInMillions, @TeamNameId) RETURNING PlayerId";
                player.PlayerId = await connection.QueryFirstOrDefaultAsync<int>(query, player);
            }

            // Act
            var result = await repository.GetTeamByPlayerId(player.PlayerId);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<FootballTeam>();
            result.TeamId.Should().Be(createdTeam.TeamId);

            // Clean up
            await repository.DeleteTeam(createdTeam.TeamId);
        }
    }
}
