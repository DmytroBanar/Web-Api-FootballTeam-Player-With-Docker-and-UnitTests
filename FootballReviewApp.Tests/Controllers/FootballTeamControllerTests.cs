using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Contracts;
using WebApi.Controllers;
using WebApi.Entities;
using Xunit;

namespace FootballReviewApp.Tests.Controllers
{
    public class FootballTeamControllerTests
    {
        private readonly IFootballTeamRepository _teamRepo;

        public FootballTeamControllerTests()
        {
            _teamRepo = FakeItEasy.A.Fake<IFootballTeamRepository>();
        }

        [Fact]
        public async Task FootballTeamController_GetTeams_ReturnsOk()
        {
            // Arrange
            // Створюємо список команд для повернення
            var teams = new List<FootballTeam>
            {
                new FootballTeam
                {
                    TeamId = 5,
                    TeamName = "FC Barcelona",
                    TeamCountry = "Spain",
                    TeamCountryRegion = "Barcelona"
                },
                new FootballTeam
                {
                    TeamId = 2,
                    TeamName = "Manchester City",
                    TeamCountry = "England",
                    TeamCountryRegion = "Manchester"
                }
            };

            A.CallTo(() => _teamRepo.GetTeams()).Returns(Task.FromResult<IEnumerable<FootballTeam>>(teams));
            var controller = new FootballTeamsController(_teamRepo);

            // Act
            // Викликаємо метод контролера
            var result = await controller.GetTeams();

            // Assert
            // Перевіряємо результат
            result.Should().NotBeNull();
            result.Result.Should().NotBeNull();
            result.Result.Should().BeOfType<OkObjectResult>();

            var okResult = result.Result as OkObjectResult;
            okResult.Should().NotBeNull();
            okResult.Value.Should().BeOfType<List<FootballTeam>>();
            var returnedTeams = okResult.Value as List<FootballTeam>;
            returnedTeams.Should().HaveCount(2); // Очікуємо 2 команди
        }
    }
}
