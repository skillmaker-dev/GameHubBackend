using Application.Interfaces.HttpClient;
using GameHubApi.DTOs;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GameHubApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlatformsController(ILogger<PlatformsController> logger,IRawgApiClient rawgApiClient) : ControllerBase
    {
        private readonly ILogger<PlatformsController> _logger = logger;
        private readonly IRawgApiClient _rawgApiClient = rawgApiClient;

        [HttpGet]
        public async Task<ActionResult<RawgFetchResponseDTO<PlatformDTO>>> GetPlatforms()
        {
            _logger.LogInformation($"Calling the {nameof(GetPlatforms)} endpoint");
            var platforms = await _rawgApiClient.GetPlatformsAsync();

            return Ok(platforms.Adapt<RawgFetchResponseDTO<PlatformDTO>>());
        }
    }
}
