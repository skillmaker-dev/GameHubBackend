using Domain.Entities;
using Domain.FetchResponses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.HttpClient
{
    public interface IRawgApiClient
    {
        /// <summary>
        /// Get all games
        /// </summary>
        /// <param name="genres">Filter by genres</param>
        /// <param name="parentPlatforms">Filter by Parent platforms</param>
        /// <param name="ordering">Ordering (name, released, added, created, updated, rating, metacritic, -reverse) </param>
        /// <param name="search">Search query</param>
        /// <param name="page">A page number within the paginated result set</param>
        /// <returns>A <see cref="RawgFetchResponse{T}"/> Object that contains a list of Games</returns>
        Task<RawgFetchResponse<Game>?> GetGamesAsync(string? genres, string? parentPlatforms, string? ordering, string? search, string? page);
        /// <summary>
        /// Get a game by slug
        /// </summary>
        /// <param name="slug">Slug of the game</param>
        /// <returns>A <see cref="Game"/> object</returns>
        Task<Game?> GetGameAsync(string slug);
        Task<IEnumerable<Genre>?> GetGenresAsync();
        Task<IEnumerable<Platform>?> GetPlatformsAsync();
        /// <summary>
        /// Get game screenshots.
        /// </summary>
        /// <param name="id">Id of the game</param>
        /// <returns>A <see cref="RawgFetchResponse{T}"/> Object that contains a list of screenshots</returns>
        Task<RawgFetchResponse<GameScreenshot>?> GetGameScreenshotsAsync(int id);
        Task<IEnumerable<GameTrailer>?> GetGameTrailersAsync();
    }
}
