using Application.Interfaces.HttpClient;
using GameHubApi.DTOs;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;

namespace GameHubApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenresController(ILogger<GenresController> logger, IRawgApiClient rawgApiClient) : ControllerBase
    {
        private readonly ILogger<GenresController> _logger = logger;
        private readonly IRawgApiClient _rawgApiClient = rawgApiClient;

        [HttpGet, OutputCache]
        public async Task<ActionResult<RawgFetchResponseDTO<GenreDTO>>> GetGenres()
        {
            _logger.LogInformation($"Calling the {nameof(GetGenres)} endpoint");
            var genres = await _rawgApiClient.GetGenresAsync();

            return Ok(genres.Adapt<RawgFetchResponseDTO<GenreDTO>>());
        }
    }
}
