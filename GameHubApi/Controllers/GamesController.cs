using Application.Services.FavoriteGames;
using Application.Services.HttpClient;
using GameHubApi.Attributes;
using GameHubApi.DTOs;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;

namespace GameHubApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GamesController(ILogger<GamesController> logger, IRawgApiClient rawgApiClient, IGamesService gamesService) : ControllerBase
    {
        private readonly ILogger<GamesController> _logger = logger;
        private readonly IRawgApiClient _rawgApiClient = rawgApiClient;
        private readonly IGamesService _gamesService = gamesService;

        [HttpGet, OutputCache]
        public async Task<ActionResult<RawgFetchResponseDTO<GameDTO>>> GetGames(string? genres, string? parent_platforms, string? ordering, string? search, string? page)
        {
            _logger.LogInformation($"Calling the {nameof(GetGames)} endpoint");
            var games = await _rawgApiClient.GetGamesAsync(genres, parent_platforms, ordering, search, page);
            return Ok(games.Adapt<RawgFetchResponseDTO<GameDTO>>());
        }

        [HttpGet("{slug}"), OutputCache]
        public async Task<ActionResult<GameDTO>> GetGame(string slug)
        {
            _logger.LogInformation($"Calling the {nameof(GetGame)} endpoint");
            var game = await _rawgApiClient.GetGameAsync(slug);
            return Ok(game.Adapt<GameDTO>());
        }

        [HttpGet("{id}/screenshots"), OutputCache]
        public async Task<ActionResult<RawgFetchResponseDTO<GameScreenshotDTO>>> GetGameScreenshots(int id)
        {
            _logger.LogInformation($"Calling the {nameof(GetGameScreenshots)} endpoint");
            var screenshots = await _rawgApiClient.GetGameScreenshotsAsync(id);
            return Ok(screenshots.Adapt<RawgFetchResponseDTO<GameScreenshotDTO>>());
        }

        [HttpGet("{id}/movies"), OutputCache]
        public async Task<ActionResult<RawgFetchResponseDTO<GameTrailerDTO>>> GetGameTrailers(int id)
        {
            _logger.LogInformation($"Calling the {nameof(GetGameTrailers)} endpoint");
            var trailers = await _rawgApiClient.GetGameTrailersAsync(id);
            return Ok(trailers.Adapt<RawgFetchResponseDTO<GameTrailerDTO>>());
        }


    }
}
