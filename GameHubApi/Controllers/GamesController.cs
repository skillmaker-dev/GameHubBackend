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
    public class GamesController(IRawgApiClient rawgApiClient) : ControllerBase
    {
        private readonly IRawgApiClient _rawgApiClient = rawgApiClient;

        [HttpGet]
        public async Task<ActionResult<RawgFetchResponseDTO<GameDTO>>> GetGames(string? genre, string? parentPlatform, string? ordering, string? search, string? page)
        {
            var games = await _rawgApiClient.GetGamesAsync(genre,parentPlatform,ordering,search,page);
            return Ok(games.Adapt<RawgFetchResponseDTO<GameDTO>>());
        }

        [HttpGet("{slug}")]
        public async Task<ActionResult<GameDTO>> GetGame(string slug)
        {
            var game = await _rawgApiClient.GetGameAsync(slug);
            return Ok(game.Adapt<GameDTO>());
        }
    }
}
