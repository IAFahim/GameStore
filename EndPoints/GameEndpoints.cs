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


    public static WebApplication MapGameEndPoints(this WebApplication app)
    {
        app.MapGet("games", GetAllGames);
        app.MapGet("games/{id}", GetGame);
        return app;
    }

    private static IResult GetGame(int id)
    {
        var game = Games.Find(g => g.Id == id);
        return game == null ? Results.NoContent() : Results.Ok(game);
    }


    private static List<GameDto> GetAllGames() => Games;
}