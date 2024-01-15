using Application.Interfaces.HttpClient;
using Domain.Entities;
using Domain.FetchResponses;
using GameHubApi.DTOs;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GameHubApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GamesController(ILogger<GamesController> logger , IRawgApiClient rawgApiClient) : ControllerBase
    {
        private readonly ILogger<GamesController> _logger = logger;
        private readonly IRawgApiClient _rawgApiClient = rawgApiClient;

        [HttpGet]
        public async Task<ActionResult<RawgFetchResponseDTO<GameDTO>>> GetGames(string? genre, string? parentPlatform, string? ordering, string? search, string? page)
        {
            _logger.LogInformation($"Calling the {nameof(GetGames)} endpoint");
            var games = await _rawgApiClient.GetGamesAsync(genre,parentPlatform,ordering,search,page);
            return Ok(games.Adapt<RawgFetchResponseDTO<GameDTO>>());
        }

        [HttpGet("{slug}")]
        public async Task<ActionResult<GameDTO>> GetGame(string slug)
        {
            _logger.LogInformation($"Calling the {nameof(GetGame)} endpoint");
            var game = await _rawgApiClient.GetGameAsync(slug);
            return Ok(game.Adapt<GameDTO>());
        }

        [HttpGet("{id}/screenshots")]
        public async Task<ActionResult<RawgFetchResponseDTO<GameScreenshotDTO>>> GetGameScreenshots(int id)
        {
            _logger.LogInformation($"Calling the {nameof(GetGameScreenshots)} endpoint");
            var screenshots = await _rawgApiClient.GetGameScreenshotsAsync(id);
            return Ok(screenshots.Adapt<RawgFetchResponseDTO<GameScreenshotDTO>>());
        }

        [HttpGet("{id}/movies")]
        public async Task<ActionResult<RawgFetchResponseDTO<GameTrailerDTO>>> GetGameTrailers(int id)
        {
            _logger.LogInformation($"Calling the {nameof(GetGameTrailers)} endpoint");
            var trailers = await _rawgApiClient.GetGameTrailersAsync(id);
            return Ok(trailers.Adapt<RawgFetchResponseDTO<GameTrailerDTO>>());
        }
    }
}
