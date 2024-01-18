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

            var gameAlreadyInFav = await GameIsInFavoritesAsync(game.Slug, userId);
            if (gameAlreadyInFav)
            {
                _logger.LogError("Game with slug {slug} is already in favorites", game.Slug);
                throw new GameIsAlreadyInFavoritesException($"Game with slug {game.Slug} is already in favorites");
            }

            FavoriteGame? favoriteGame;
            favoriteGame = await _dbContext.FavoriteGames.FirstOrDefaultAsync(fg => fg.Slug == game.Slug);

            if (favoriteGame is null)
            {
                favoriteGame = new FavoriteGame
                {
                    Name = game.Name,
                    Slug = game.Slug,
                    Background_Image = game.Background_Image
                };
                await _dbContext.FavoriteGames.AddAsync(favoriteGame);
            }
            else
            {
                favoriteGame.Name = game.Name;
                favoriteGame.Background_Image = game.Background_Image;
            }



            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == userId);

            user!.FavoriteGames.Add(favoriteGame); // User is already checked if null or not when calling GameIsInFavoritesAsync above

            if (!(await _dbContext.SaveChangesAsync() > 0))
            {
                _logger.LogCritical("Could not save game with slug {slug} to the database", game.Slug);
                throw new GameNotSavedToFavoritesException($"Could not save game with slug {game.Slug} to the database");
            }
        }

        public async Task<bool> GameIsInFavoritesAsync(string slug, Guid userId)
        {
            _logger.LogInformation("Checking if game exists in favorites");
            var user = await _dbContext.Users.Include(u => u.FavoriteGames).FirstOrDefaultAsync(u => u.Id == userId);
            if (user is null)
            {
                _logger.LogCritical("Could not find user with id: {userId}", userId);
                throw new UserNotFoundException($"Could not find user with id: {userId}");
            }
            var gameExistsInFavorites = user.FavoriteGames.Any(fg => fg.Slug == slug);

            return gameExistsInFavorites;
        }

        public async Task<IEnumerable<FavoriteGame>> GetFavoriteGamesAsync(Guid userId)
        {
            _logger.LogInformation("Getting favorite games");
            var user = await _dbContext.Users.Include(u => u.FavoriteGames).FirstOrDefaultAsync(u => u.Id == userId);
            if (user is null)
            {
                _logger.LogCritical("Could not find user with id: {userId}", userId);
                throw new UserNotFoundException($"Could not find user with id: {userId}");
            }

            var favGames = user.FavoriteGames;

            return favGames ?? Enumerable.Empty<FavoriteGame>();
        }

        public async Task RemoveGameFromFavoritesAsync(string slug, Guid userId)
        {
            _logger.LogInformation("Removing a game from favorites");
            var gameAlreadyInFav = await GameIsInFavoritesAsync(slug, userId);
            if (!gameAlreadyInFav)
            {
                _logger.LogError("Game with slug {slug} is not in favorites", slug);
                throw new GameIsNotInFavoritesException($"Game with slug {slug} is not in favorites");
            }

            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == userId);

            var favoriteGame = user!.FavoriteGames.FirstOrDefault(fg => fg.Slug == slug); // User is already checked if null or not when calling GameIsInFavoritesAsync above

            user.FavoriteGames.Remove(favoriteGame!); // We already checked if the game exists in GameIsInFavoritesAsync() above

            if (!(await _dbContext.SaveChangesAsync() > 0))
            {
                _logger.LogCritical("Could not remove game with slug {slug} from the database", slug);
                throw new GameNotRemovedFromFavoritesException($"Could not remove game with slug {slug} from the database");
            }
        }
    }
}
