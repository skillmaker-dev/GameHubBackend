using Domain.Entities;
using Infrastructure.Data;
using Infrastructure.Exceptions;
using Infrastructure.Identity;
using Infrastructure.Services.Games;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Logging;
using Microsoft.VisualBasic;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    public class GamesServiceTests
    {
        private readonly ApplicationUser _testUser;
        private readonly DbContextOptions<ApplicationDbContext> dbContextOptions;
        private readonly ApplicationDbContext dbContext;

        public GamesServiceTests()
        {
            // Create a new ApplicationUser with a specific ID for testing
            _testUser = new ApplicationUser { Id = Guid.NewGuid()};
            dbContextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "MyBlogDb")
            .Options;

            dbContext = new ApplicationDbContext(dbContextOptions);
            dbContext.Users.Add(_testUser);
            dbContext.SaveChanges();
        }
        [Fact]
        public async Task AddGameToFavoritesAsync_GameNotInFavorites_AddsGame()
        {
            // Arrange
            var logger = Substitute.For<ILogger<GamesService>>();
            var service = new GamesService(logger, dbContext);

            var game = new Game { Slug = "new-game", Name = "New Game" };

            // Act
            var exception = Record.ExceptionAsync(async () => await service.AddGameToFavoritesAsync(game, _testUser.Id));

            // Assert
            Assert.Null(await exception);
        }

        [Fact]
        public async Task AddGameToFavoritesAsync_GameAlreadyInFavorites_ThrowsException()
        {
            // Arrange
            var logger = Substitute.For<ILogger<GamesService>>();
            var service = new GamesService(logger, dbContext);

            var game = new Game { Slug = "existing-game", Name = "Existing Game" };
            var favoriteGame = new FavoriteGame { Slug = game.Slug, Name = game.Name };
            _testUser.FavoriteGames.Add(favoriteGame);
            await dbContext.SaveChangesAsync();

            // Act and Assert
            await Assert.ThrowsAsync<GameIsAlreadyInFavoritesException>(() => service.AddGameToFavoritesAsync(game, _testUser.Id));
        }


        [Fact]
        public async Task GameIsInFavoritesAsync_GameExistsInFavorites_ReturnsTrue()
        {
            // Arrange
            var logger = Substitute.For<ILogger<GamesService>>();
            var service = new GamesService(logger, dbContext);

            var favoriteGame = new FavoriteGame { Slug = "existing-game", Name = "Existing Game" };
            _testUser.FavoriteGames.Add(favoriteGame);
            await dbContext.SaveChangesAsync();

            // Act
            var result = await service.GameIsInFavoritesAsync("existing-game", _testUser.Id);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task GameIsInFavoritesAsync_UserNotFound_ThrowsException()
        {
            // Arrange
            var logger = Substitute.For<ILogger<GamesService>>();
            var service = new GamesService(logger, dbContext);

            var favoriteGame = new FavoriteGame { Slug = "existing-game", Name = "Existing Game" };
            _testUser.FavoriteGames.Add(favoriteGame);
            await dbContext.SaveChangesAsync();

            // Act and Assert
            await Assert.ThrowsAsync<UserNotFoundException>(() => service.GameIsInFavoritesAsync("existing-game", Guid.NewGuid()));
        }

        [Fact]
        public async Task GetFavoriteGamesAsync_UserExists_ReturnsFavoriteGames()
        {
            // Arrange
            var logger = Substitute.For<ILogger<GamesService>>();
            var service = new GamesService(logger, dbContext);

            var favoriteGame = new FavoriteGame { Slug = "existing-game", Name = "Existing Game" };
            _testUser.FavoriteGames.Add(favoriteGame);
            await dbContext.SaveChangesAsync();

            // Act
            var result = await service.GetFavoriteGamesAsync(_testUser.Id);

            // Assert
            Assert.Single(result);
        }

        [Fact]
        public async Task GetFavoriteGamesAsync_UserNotFound_ThrowException()
        {
            // Arrange
            var logger = Substitute.For<ILogger<GamesService>>();
            var service = new GamesService(logger, dbContext);

            var favoriteGame = new FavoriteGame { Slug = "existing-game", Name = "Existing Game" };
            _testUser.FavoriteGames.Add(favoriteGame);
            await dbContext.SaveChangesAsync();

            // Act and Assert
            await Assert.ThrowsAsync<UserNotFoundException>(() => service.GetFavoriteGamesAsync(Guid.NewGuid()));
        }

        [Fact]
        public async Task RemoveGameFromFavoritesAsync_GameInFavorites_RemovesGame()
        {
            // Arrange
            var logger = Substitute.For<ILogger<GamesService>>();
            var service = new GamesService(logger, dbContext);

            var favoriteGame = new FavoriteGame { Slug = "existing-game", Name = "Existing Game" };
            _testUser.FavoriteGames.Add(favoriteGame);
            await dbContext.SaveChangesAsync();

            // Act
            var exception = Record.ExceptionAsync(async () => await service.RemoveGameFromFavoritesAsync(favoriteGame.Slug, _testUser.Id));

            // Assert
            Assert.Null(await exception);
        }

        [Fact]
        public async Task RemoveGameFromFavoritesAsync_GameNotInFavorites_ThrowsException()
        {
            // Arrange
            var logger = Substitute.For<ILogger<GamesService>>();
            var service = new GamesService(logger, dbContext);

            _testUser.FavoriteGames = new List<FavoriteGame>();
            await dbContext.SaveChangesAsync();

            // Act and Assert
            await Assert.ThrowsAsync<GameIsNotInFavoritesException>(() => service.RemoveGameFromFavoritesAsync("random-slug", _testUser.Id));
        }
    }
}
