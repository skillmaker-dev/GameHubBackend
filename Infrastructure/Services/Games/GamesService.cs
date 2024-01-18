using Application.Services.FavoriteGames;
using Domain.Entities;
using Infrastructure.Data;
using Infrastructure.Exceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services.Games
{
    public class GamesService(ILogger<GamesService> logger, ApplicationDbContext dbContext) : IGamesService
    {
        private readonly ILogger<GamesService> _logger = logger;
        private readonly ApplicationDbContext _dbContext = dbContext;

        public async Task AddGameToFavoritesAsync(Game game, Guid userId)
        {
            _logger.LogInformation("Adding a game to favorites");
            
            var favoriteGame = new FavoriteGame
            {
                UserId = userId,
                Name = game.Name,
                Slug = game.Slug
            };

            var gameAlreadyInFav = await GameIsInFavoritesAsync(game.Slug, userId);
            if(gameAlreadyInFav)
            {
                _logger.LogError("Game with slug {slug} is already in favorites", game.Slug);
                throw new GameIsAlreadyInFavoritesException($"Game with slug {game.Slug} is already in favorites");
            }

            await _dbContext.FavoriteGames.AddAsync(favoriteGame);

            if(!(await _dbContext.SaveChangesAsync() > 0)) 
            {
                _logger.LogCritical("Could not save game with slug {slug} to the database",game.Slug);
                throw new GameNotSavedToFavoritesException($"Could not save game with slug {game.Slug} to the database");
            }
        }

        public async Task<bool> GameIsInFavoritesAsync(string slug, Guid userId)
        {
            return await _dbContext.FavoriteGames.AnyAsync(fg => (fg.Slug == slug && fg.UserId == userId));
        }

        public async Task<IEnumerable<FavoriteGame>> GetFavoriteGamesAsync(Guid userId)
        {
            _logger.LogInformation("Getting favorite games");
            var favGames = await _dbContext.FavoriteGames.Where(fg => fg.UserId == userId).ToListAsync();
            if(favGames is null)
                return Enumerable.Empty<FavoriteGame>();

            return favGames;
        }

        public async Task RemoveGameFromFavoritesAsync(string slug, Guid userId)
        {
            _logger.LogInformation("Removing a game from favorites");
            var favGame = await _dbContext.FavoriteGames.FirstOrDefaultAsync(fg => (fg.Slug == slug && fg.UserId == userId));
            if (favGame is null)
            {
                _logger.LogError("Game with slug {slug} is not in favorites", slug);
                throw new GameIsNotInFavoritesException($"Game with slug {slug} is not in favorites");
            }

            _dbContext.FavoriteGames.Remove(favGame);

            if (!(await _dbContext.SaveChangesAsync() > 0))
            {
                _logger.LogCritical("Could not remove game with slug {slug} from the database", slug);
                throw new GameNotRemovedFromFavoritesException($"Could not remove game with slug {slug} from the database");
            }
        }
    }
}
