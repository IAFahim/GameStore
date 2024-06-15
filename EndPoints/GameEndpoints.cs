using GameStore.API.Dtos;

namespace GameStore.API.EndPoints;

public static class GameEndpoints
{
    const string GetGameRouteName = "GetGame";

    private static readonly List<GameDto> Games =
    [
        new GameDto(1, "Super Mario", "Platformer", 59.99m, new DateOnly(1985, 9, 13)),
        new GameDto(2, "The Legend of Zelda", "Action-adventure", 59.99m, new DateOnly(1986, 2, 21)),
        new GameDto(3, "Minecraft", "Sandbox", 26.95m, new DateOnly(2011, 11, 18))
    ];

    public static RouteGroupBuilder MapGameEndPoints(this WebApplication app)
    {
        var mapGroup = app.MapGroup("games");
        {
            mapGroup.MapGet("/", GetAllGames);
            mapGroup.MapGet("/{id}", GetGame).WithName(GetGameRouteName);
            mapGroup.MapPost("/", AddGame);
            mapGroup.MapPut("/{id}", UpdateGame);
            mapGroup.MapDelete("/{id}", RemoveGame);
        }
        return mapGroup.WithParameterValidation();
    }

    private static IResult RemoveGame(int id)
    {
        var deleted = Games.RemoveAll(g => g.Id == id);
        if (deleted == 0) return Results.NotFound();
        return Results.NoContent();
    }

    private static IResult UpdateGame(int id, UpdateGameDto updateGameDto)
    {
        var index = Games.FindIndex(g => g.Id == id);
        if (index == -1) return Results.NotFound();

        Games[index] = new GameDto(
            Id: id, Name: updateGameDto.Name,
            Genre: updateGameDto.Genre,
            Price: updateGameDto.Price,
            ReleaseDate: updateGameDto.ReleaseDate
        );

        return Results.NoContent();
    }

    private static IResult AddGame(CreateGameDto newGame)
    {
        GameDto gameDto = new GameDto(
            Id: Games.Count + 1,
            Name: newGame.Name,
            Genre: newGame.Genre,
            Price: newGame.Price,
            ReleaseDate: newGame.ReleaseDate
        );
        Games.Add(gameDto);
        return Results.CreatedAtRoute(GetGameRouteName, new { id = gameDto.Id }, gameDto);
    }

    private static IResult GetGame(int id)
    {
        var game = Games.Find(g => g.Id == id);
        return game == null ? Results.NoContent() : Results.Ok(game);
    }

    private static List<GameDto> GetAllGames() => Games;
}