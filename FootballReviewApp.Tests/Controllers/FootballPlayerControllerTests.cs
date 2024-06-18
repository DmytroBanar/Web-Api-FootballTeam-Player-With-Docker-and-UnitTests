using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebApi.Contracts;
using WebApi.Controllers;
using WebApi.Dto;
using WebApi.Entities;
using Xunit;

namespace FootballReviewApp.Tests.Controllers
{
    public class FootballPlayerControllerTests
    {
        private readonly IPlayerRepository _teamRepo;

        public FootballPlayerControllerTests()
        {
            _teamRepo = A.Fake<IPlayerRepository>();
        }

        [Fact]
        public async Task FootballPlayerController_CreatePlayer_ReturnsCreatedAtRoute()
        {
            // Arrange
            var playerToCreate = new FootballPlayerForCreationDto
            {
                PlayerId = 1,
                PlayerName = "Lionel",
                PlayerSurname = "Messi",
                PlayerAge = 36,
                TeamName = "FC Barcelona",
                PlayerCountry = "Argentina",
                PlayerPosition = "RW",
                PlayerCostInMillions = 30,
                TeamNameId = 5
            };

            var createdPlayer = new Player
            {
                PlayerId = 1,
                PlayerName = "Lionel",
                PlayerSurname = "Messi",
                PlayerAge = 36,
                TeamName = "FC Barcelona",
                PlayerCountry = "Argentina",
                PlayerPosition = "RW",
                PlayerCostInMillions = 30,
                TeamNameId = 5
            };

            A.CallTo(() => _teamRepo.CreatePlayer(A<FootballPlayerForCreationDto>.Ignored)).Returns(Task.FromResult(createdPlayer));
            var controller = new PlayerController(_teamRepo);

            // Act
            var result = await controller.CreatePlayer(playerToCreate);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<CreatedAtRouteResult>();

            var createdAtRouteResult = result as CreatedAtRouteResult;
            createdAtRouteResult.Should().NotBeNull();
            createdAtRouteResult.RouteName.Should().Be("PlayerById");
            createdAtRouteResult.RouteValues.Should().ContainKey("id");
            createdAtRouteResult.RouteValues["id"].Should().Be(createdPlayer.PlayerId);

            createdAtRouteResult.Value.Should().Be(createdPlayer);
        }
    }
}
