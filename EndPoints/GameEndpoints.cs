using GameStore.API.Data;
using GameStore.API.Dtos;
using GameStore.API.Entities;
using GameStore.API.Mapping;
using Microsoft.EntityFrameworkCore;

namespace GameStore.API.EndPoints;

public static class GameEndpoints
{
    const string GetGameRouteName = "GetGame";

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

    private static IResult RemoveGame(int id, GameStoreContext gameStoreContext)
    {
        var deleteCount = gameStoreContext.Games
            .Where(g => g.Id == id)
            .ExecuteDelete();

        if (deleteCount == 0) return Results.NotFound();
        return Results.NoContent();
    }

    private static IResult UpdateGame(int id, UpdateGameDto updateGameDto, GameStoreContext gameStoreContext)
    {
        Game? existingGame = gameStoreContext.Games.Find(id);
        if (existingGame == null) return Results.NotFound();

        Game updateGame = updateGameDto.ToEntity(existingGame.Id);
        gameStoreContext.Entry(existingGame).CurrentValues.SetValues(updateGame);
        gameStoreContext.SaveChanges();

        return Results.NoContent();
    }

    private static IResult AddGame(CreateGameDto newGame, GameStoreContext gameStoreContext)
    {
        Game game = newGame.ToEntity();

        gameStoreContext.Games.Add(game);
        gameStoreContext.SaveChanges();

        return Results.CreatedAtRoute(
            GetGameRouteName,
            new { id = game.Id },
            game.ToGameDetailsDto()
        );
    }

    private static IResult GetGame(int id, GameStoreContext gameStoreContext)
    {
        Game? game = gameStoreContext.Games.Find(id);
        return game == null ? Results.NoContent() : Results.Ok(game.ToGameDetailsDto());
    }

    private static IResult GetAllGames(GameStoreContext gameStoreContext)
    {
        IQueryable<GameSummaryDto> gameSummaryDtos = gameStoreContext.Games
            .Include(g => g.Genre)
            .Select(g => g.ToGameSummaryDto())
            .AsNoTracking();
        return Results.Ok(gameSummaryDtos);
    }
}