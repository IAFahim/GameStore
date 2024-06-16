using GameStore.API.Dtos;
using GameStore.API.Entities;

namespace GameStore.API.Mapping;

public static class GameMapping
{
    public static Game ToEntity(this CreateGameDto game)
    {
        return new Game()
        {
            Name = game.Name,
            GenreId = game.GenreId,
            Price = game.Price,
            ReleaseDate = game.ReleaseDate
        };
    }

    public static GameDto ToDto(this Game game)
    {
        return new GameDto(
            Id: game.Id,
            Name: game.Name,
            Genre: game.Genre!.Name,
            Price: game.Price,
            ReleaseDate: game.ReleaseDate
        );
    }
}