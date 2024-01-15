using Application.Interfaces.HttpClient;
using Domain.Entities;
using Domain.FetchResponses;
using Infrastructure.Exceptions;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

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
            var response = await _httpClient.GetAsync($"games/{slug}?key={_key}");

            if (response.IsSuccessStatusCode)
            {
                var game = await response.Content.ReadFromJsonAsync<Game>();
                return game;
            }
            else if (response.StatusCode == HttpStatusCode.NotFound)
            {
                throw new RawgApiException("Could not find the game",response.StatusCode);
            }
            else if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                throw new RawgApiException("Could not make call to the api, unauthorized", response.StatusCode);
            }
            else
            {
                throw new RawgApiException("An error occured when calling the api",HttpStatusCode.InternalServerError);
            }
        }

        public async Task<RawgFetchResponse<Game>?> GetGamesAsync(string? genres, string? parentPlatforms, string? ordering, string? search, string? page)
        {
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
                throw new RawgApiException("Could not find the games", response.StatusCode);
            }
            else if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                throw new RawgApiException("Could not make call to the api, unauthorized", response.StatusCode);
            }
            else
            {
                throw new RawgApiException("An error occured when calling the api", HttpStatusCode.InternalServerError);
            }
        }

        public Task<IEnumerable<GameScreenshot>?> GetGameScreenshotsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<GameTrailer>?> GetGameTrailersAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Genre>?> GetGenresAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Platform>?> GetPlatformsAsync()
        {
            throw new NotImplementedException();
        }
    }
}
