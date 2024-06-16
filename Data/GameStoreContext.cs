using GameStore.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace GameStore.API.Data;

public class GameStoreContext(DbContextOptions<GameStoreContext> options) : DbContext(options)
{
    public DbSet<Game> Games => Set<Game>();
    public DbSet<Genre> Genres => Set<Genre>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        Genre[] genres =
        [
            new Genre { Id = 1, Name = "Fighting" },
            new Genre { Id = 2, Name = "RolePlaying" },
            new Genre { Id = 3, Name = "Sports" },
            new Genre { Id = 4, Name = "Racing" },
            new Genre { Id = 5, Name = "Kids and Family" }
        ];
        modelBuilder.Entity<Genre>().HasData(genres);

        Game[] games =
        [
            new Game
            {
                Id = 1, Name = "Street Fighter V", GenreId = 1, Price = 59.99m, ReleaseDate = new DateOnly(2016, 2, 16)
            },
            new Game
            {
                Id = 2, Name = "Final Fantasy VII Remake", GenreId = 2, Price = 59.99m,
                ReleaseDate = new DateOnly(2020, 4, 10)
            },
            new Game { Id = 3, Name = "FIFA 21", GenreId = 3, Price = 59.99m, ReleaseDate = new DateOnly(2020, 10, 9) },
            new Game
            {
                Id = 4, Name = "Gran Turismo Sport", GenreId = 4, Price = 19.99m,
                ReleaseDate = new DateOnly(2017, 10, 17)
            },
            new Game
            {
                Id = 5, Name = "Super Mario Odyssey", GenreId = 5, Price = 59.99m,
                ReleaseDate = new DateOnly(2017, 10, 27)
            }
        ];
        modelBuilder.Entity<Game>().HasData(games);
    }
}