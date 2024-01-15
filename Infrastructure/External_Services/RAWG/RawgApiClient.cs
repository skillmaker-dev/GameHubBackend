using Application.Interfaces.HttpClient;
using Domain.Entities;
using Domain.FetchResponses;
using Infrastructure.Exceptions;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Net.Http.Json;

namespace Infrastructure.External_Services.RAWG
{
    public class RawgApiClient : IRawgApiClient
    {
        private readonly ILogger<RawgApiClient> _logger;
        private readonly HttpClient _httpClient;
        private readonly string? _key;

        public RawgApiClient(ILogger<RawgApiClient> logger, HttpClient httpClient, IConfiguration configuration)
        {
            _logger = logger;
            _httpClient = httpClient;
            var baseUrl = configuration.GetSection("RAWG:BaseUrl").Value;
            if (baseUrl is null)
            {
                _logger.LogCritical("Base url of RAWG Api is not found");
                throw new Exception("Base url of RAWG Api is not found");
            }
            _key = configuration.GetSection("RAWG:Key").Value;
            if (_key is null)
            {
                _logger.LogCritical("key of RAWG Api is not found");
                throw new Exception("key of RAWG Api is not found");
            }

            _httpClient.BaseAddress = new Uri(baseUrl);
        }

        public async Task<Game?> GetGameAsync(string slug)
        {
            _logger.LogInformation("Attempting to get Game with slug {slug}", slug);
            var response = await _httpClient.GetAsync($"games/{slug}?key={_key}");

            if (response.IsSuccessStatusCode)
            {
                var game = await response.Content.ReadFromJsonAsync<Game>();
                return game;
            }
            else if (response.StatusCode == HttpStatusCode.NotFound)
            {
                _logger.LogError("Could not find the game with slug {slug}", slug);
                throw new GameNotFoundException("Could not find the game");
            }
            else if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                _logger.LogCritical("Could not make call to the api, unauthorized");
                throw new RawgApiException("Could not make call to the api", HttpStatusCode.InternalServerError);
            }
            else
            {
                _logger.LogCritical("An error occured when calling the api");
                throw new RawgApiException("An error occured when calling the api", HttpStatusCode.InternalServerError);
            }
        }

        public async Task<RawgFetchResponse<Game>?> GetGamesAsync(string? genres, string? parentPlatforms, string? ordering, string? search, string? page)
        {
            _logger.LogInformation("Attempting to get Games list");
            var queryParams = new Dictionary<string, string?>()
            {
                { "genres", genres },
                { "parentPlatforms", parentPlatforms },
                {"ordering", ordering },
                {"search", search},
                {"key", _key},
                {"page",  page},
            };
            var url = QueryHelpers.AddQueryString("games", queryParams);
            var response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var gamesResponse = await response.Content.ReadFromJsonAsync<RawgFetchResponse<Game>>();
                return gamesResponse;
            }
            else if (response.StatusCode == HttpStatusCode.NotFound)
            {
                _logger.LogError("Could not find the games");
                throw new GamesListNotFoundException("Could not find the games");
            }
            else if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                _logger.LogCritical("Could not make call to the api, unauthorized");
                throw new RawgApiException("Could not make call to the api", HttpStatusCode.InternalServerError);
            }
            else
            {
                _logger.LogCritical("An error occured when calling the api");
                throw new RawgApiException("An error occured when calling the api", HttpStatusCode.InternalServerError);
            }
        }

        public async Task<RawgFetchResponse<GameScreenshot>?> GetGameScreenshotsAsync(int id)
        {
            _logger.LogInformation("Attempting to get Game screenshots");
            var response = await _httpClient.GetAsync($"games/{id}/screenshots?key={_key}");

            if (response.IsSuccessStatusCode)
            {
                var screenshotsResponse = await response.Content.ReadFromJsonAsync<RawgFetchResponse<GameScreenshot>>();
                return screenshotsResponse;
            }
            else if (response.StatusCode == HttpStatusCode.NotFound)
            {
                _logger.LogError("Could not find screenshots for the game with id {id}", id);
                throw new GameScreenshotsNotFoundException($"Could not find screenshots for the game with id {id}");
            }
            else if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                _logger.LogCritical("Could not make call to the api, unauthorized");
                throw new RawgApiException("Could not make call to the api", HttpStatusCode.InternalServerError);
            }
            else
            {
                _logger.LogCritical("An error occured when calling the api");
                throw new RawgApiException("An error occured when calling the api", HttpStatusCode.InternalServerError);
            }
        }

        public async Task<RawgFetchResponse<GameTrailer>?> GetGameTrailersAsync(int id)
        {
            _logger.LogInformation("Attempting to get Game trailers");
            var response = await _httpClient.GetAsync($"games/{id}/movies?key={_key}");

            if (response.IsSuccessStatusCode)
            {
                var trailersResponse = await response.Content.ReadFromJsonAsync<RawgFetchResponse<GameTrailer>>();
                return trailersResponse;
            }
            else if (response.StatusCode == HttpStatusCode.NotFound)
            {
                _logger.LogError("Could not find trailers for the game with id {id}", id);
                throw new GameTrailersNotFoundException($"Could not find trailers for the game with id {id}");
            }
            else if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                _logger.LogCritical("Could not make call to the api, unauthorized");
                throw new RawgApiException("Could not make call to the api", HttpStatusCode.InternalServerError);
            }
            else
            {
                _logger.LogCritical("An error occured when calling the api");
                throw new RawgApiException("An error occured when calling the api", HttpStatusCode.InternalServerError);
            }
        }

        public async Task<RawgFetchResponse<Genre>?> GetGenresAsync()
        {
            _logger.LogInformation("Attempting to get genres");
            var response = await _httpClient.GetAsync($"genres?key={_key}");

            if (response.IsSuccessStatusCode)
            {
                var genresResponse = await response.Content.ReadFromJsonAsync<RawgFetchResponse<Genre>>();
                return genresResponse;
            }
            else if (response.StatusCode == HttpStatusCode.NotFound)
            {
                _logger.LogError("Could not find genres");
                throw new GenresNotFoundException("Could not find genres");
            }
            else if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                _logger.LogCritical("Could not make call to the api, unauthorized");
                throw new RawgApiException("Could not make call to the api", HttpStatusCode.InternalServerError);
            }
            else
            {
                _logger.LogCritical("An error occured when calling the api");
                throw new RawgApiException("An error occured when calling the api", HttpStatusCode.InternalServerError);
            }
        }

        public async Task<RawgFetchResponse<Platform>?> GetPlatformsAsync()
        {
            _logger.LogInformation("Attempting to get platforms");
            var response = await _httpClient.GetAsync($"platforms/lists/parents?key={_key}");

            if (response.IsSuccessStatusCode)
            {
                var platformsResponse = await response.Content.ReadFromJsonAsync<RawgFetchResponse<Platform>>();
                return platformsResponse;
            }
            else if (response.StatusCode == HttpStatusCode.NotFound)
            {
                _logger.LogError("Could not find platforms");
                throw new PlatformsNotFoundException("Could not find platforms");
            }
            else if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                _logger.LogCritical("Could not make call to the api, unauthorized");
                throw new RawgApiException("Could not make call to the api", HttpStatusCode.InternalServerError);
            }
            else
            {
                _logger.LogCritical("An error occured when calling the api");
                throw new RawgApiException("An error occured when calling the api", HttpStatusCode.InternalServerError);
            }
        }
    }
}
