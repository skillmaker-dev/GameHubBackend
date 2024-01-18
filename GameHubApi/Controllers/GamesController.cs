using Application.Services.FavoriteGames;
using Application.Services.HttpClient;
using Domain.Entities;
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

        [HttpGet("{slug}"),Authorize, OutputCache, GetCurrentUserId]
        public async Task<ActionResult<GameDTO>> GetGame(string slug)
        {
            _logger.LogInformation($"Calling the {nameof(GetGame)} endpoint");
            var currentUserId = HttpContext.Items["CurrentUserId"] as string;
            var game = await _rawgApiClient.GetGameAsync(slug);

            var gameIsInFavorite = await _gamesService.GameIsInFavoritesAsync(slug, Guid.Parse(currentUserId!));

            var gameDto = game.Adapt<GameDTO>();
            gameDto.IsInFavorites = gameIsInFavorite;

            return Ok(gameDto);
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

        [HttpGet("favoriteGames"),Authorize,GetCurrentUserId]
        public async Task<ActionResult<IEnumerable<FavoriteGameDTO>>> GetFavoriteGames()
        {
            _logger.LogInformation($"Calling the {nameof(GetFavoriteGames)} endpoint");
            var currentUserId = HttpContext.Items["CurrentUserId"] as string;

            var favoriteGames = await _gamesService.GetFavoriteGamesAsync(Guid.Parse(currentUserId!));
            return Ok(favoriteGames.Adapt<IEnumerable<FavoriteGameDTO>>());
        }

        [HttpPost("favoriteGames"), Authorize, GetCurrentUserId]
        public async Task<IActionResult> AddFavoriteGame(GameDTO gamedto)
        {
            _logger.LogInformation($"Calling the {nameof(AddFavoriteGame)} endpoint");
            var currentUserId = HttpContext.Items["CurrentUserId"] as string;

            var game = gamedto.Adapt<Game>();

            await _gamesService.AddGameToFavoritesAsync(game, Guid.Parse(currentUserId!));
            return Created();
        }

        [HttpDelete("favoriteGames/{slug}"), Authorize, GetCurrentUserId]
        public async Task<IActionResult> RemoveGameFromFavorite(string slug)
        {
            _logger.LogInformation($"Calling the {nameof(RemoveGameFromFavorite)} endpoint");
            var currentUserId = HttpContext.Items["CurrentUserId"] as string;

            await _gamesService.RemoveGameFromFavoritesAsync(slug, Guid.Parse(currentUserId!));
            return NoContent();
        }
    }
}
