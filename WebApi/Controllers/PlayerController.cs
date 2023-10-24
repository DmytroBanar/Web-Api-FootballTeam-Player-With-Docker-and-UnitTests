using Microsoft.AspNetCore.Mvc;
using WebApi.Contracts;
using WebApi.Dto;
using WebApi.Entities;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlayersController : ControllerBase
    {
        private readonly IPlayerRepository _teamRepo;

        public PlayersController(IPlayerRepository teamRepo)
        {
            _teamRepo = teamRepo;
        }


        [HttpGet("GetAllPlayers")]
        public async Task<ActionResult<IEnumerable<Player>>> GetPlayers()
        {
            try
            {
                var players = await _teamRepo.GetPlayers();
                return Ok(players);
            }
            catch (Exception ex)
            {
                // Log error
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("GetPlayer/{id}", Name = "PlayerById")]
        public async Task<IActionResult> GetPlayer(int id)
        {
            try
            {
                var player = await _teamRepo.GetPlayer(id);
                if (player == null)
                    return NotFound();

                return Ok(player);
            }
            catch (Exception ex)
            {
                // Log error
                return StatusCode(500, ex.Message);
            }
        }


        [HttpPost("CreatePlayer")]
        public async Task<IActionResult> CreatePlayer(FootballPlayerForCreationDto player)
        {
            try
            {
                var createdPlayer = await _teamRepo.CreatePlayer(player);
                return CreatedAtRoute("PlayerById", new { id = createdPlayer.PlayerId }, createdPlayer);
            }

            catch (Exception ex)
            {
                // Log error
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("UpdatePlayer/{id}")]
        public async Task<IActionResult> UpdatePlayer(int id, FootballPlayerForUpdateDto player)
        {
            try
            {
                var dbPlayer = await _teamRepo.GetPlayer(id);
                if (dbPlayer == null)
                    return NotFound();

                await _teamRepo.UpdatePlayer(id, player);
                return NoContent();
            }
            catch (Exception ex)
            {
                // Log error
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("DeletePlayer/{id}")]
        public async Task<IActionResult> DeletePlayer(int id)
        {
            try
            {
                var dbPlayer = await _teamRepo.GetPlayer(id);
                if (dbPlayer == null)
                    return NotFound();

                await _teamRepo.DeletePlayer(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                // Log error
                return StatusCode(500, ex.Message);
            }
        }

    }

}