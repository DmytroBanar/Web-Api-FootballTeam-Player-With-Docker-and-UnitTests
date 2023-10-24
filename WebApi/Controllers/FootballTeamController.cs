using Microsoft.AspNetCore.Mvc;
using WebApi.Contracts;
using WebApi.Dto;
using WebApi.Entities;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FootballTeamsController : ControllerBase
    {
        private readonly IFootballTeamRepository _teamRepo;

        public FootballTeamsController(IFootballTeamRepository teamRepo)
        {
            _teamRepo = teamRepo;
        }

        [HttpGet("GetAllTeams")]
        public async Task<ActionResult<IEnumerable<FootballTeam>>>GetTeams()
        {
            try
            {
                var teams = await _teamRepo.GetTeams();
                return Ok(teams);
    }
            catch (Exception ex)
            {
                // Log error
                return StatusCode(500, ex.Message);
}
        }

        [HttpGet("{id}", Name = "TeamById")]
public async Task<IActionResult> GetTeam(int id)
{
    try
    {
        var team = await _teamRepo.GetTeam(id);
        if (team == null)
            return NotFound();

        return Ok(team);
    }
    catch (Exception ex)
    {
        // Log error
        return StatusCode(500, ex.Message);
    }
}

        [HttpPost]
                public async Task<IActionResult> CreateTeam(FootballTeamForCreationDto team)
                {
                    try
                    {
                        var createdTeam = await _teamRepo.CreateTeam(team);
                        return CreatedAtRoute("TeamById", new { id = createdTeam.TeamId }, createdTeam);
                    }
                    catch (Exception ex)
                    {
                        // Log error
                        return StatusCode(500, ex.Message);
                    }
                }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTeam(int id, FootballTeamForUpdateDto team)
        {
            try
            {
                var dbTeam = await _teamRepo.GetTeam(id);
                if (dbTeam == null)
                    return NotFound();

                await _teamRepo.UpdateTeam(id, team);
                return NoContent();
            }
            catch (Exception ex)
            {
                // Log error
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("DeleteTeam/{id}")]
        public async Task<IActionResult> DeleteTeam(int id)
        {
            try
            {
                var dbTeam = await _teamRepo.GetTeam(id);
                if (dbTeam == null)
                    return NotFound();

                await _teamRepo.DeleteTeam(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                // Log error
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("ByPlayerId/{id}")]
        public async Task<IActionResult> GetTeamForPlayer(int id)
        {
            try
            {
                var team = await _teamRepo.GetTeamByPlayerId(id);
                if (team == null)
                    return NotFound();

                return Ok(team);
            }
            catch (Exception ex)
            {
                // Log error
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("GetAllPlayers")]
        public async Task<ActionResult<IEnumerable<FootballTeam>>> GetPlayers()
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

        // Add similar actions for multiple results and mapping if needed


    }
}
