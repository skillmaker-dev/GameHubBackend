using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.FavoriteGames
{
    public interface IGamesService
    {
        /// <summary>
        /// Get favorites games of a user
        /// </summary>
        /// <param name="userId">User id</param>
        /// <returns>an <see cref="IEnumerable{FavoriteGame}"/> of favorite games</returns>
        Task<IEnumerable<FavoriteGame>> GetFavoriteGamesAsync(Guid userId);
        /// <summary>
        /// Add a game to favorites
        /// </summary>
        /// <param name="game">Game to add to favorites</param>
        /// <param name="userId">User id</param>
        Task AddGameToFavoritesAsync(Game game, Guid userId);
        /// <summary>
        /// Remove a game from favorites
        /// </summary>
        /// <param name="slug">Slug of the game to remove</param>
        /// <param name="userId">User id</param>
        Task RemoveGameFromFavoritesAsync(string slug, Guid userId);
        /// <summary>
        /// See if a game is in favorites
        /// </summary>
        /// <param name="slug">Slug of the game</param>
        /// <param name="userId">User id</param>
        /// <returns>a <see cref="bool"/> value indicating if the game is in favorites</returns>
        Task<bool> GameIsInFavoritesAsync(string slug,Guid userId);
    }
}
