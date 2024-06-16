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
            mapGroup.MapGet("/", GetAllGamesAsync);
            mapGroup.MapGet("/{id}", GetGameAsync).WithName(GetGameRouteName);
            mapGroup.MapPost("/", AddGameAsync);
            mapGroup.MapPut("/{id}", UpdateGameAsync);
            mapGroup.MapDelete("/{id}", RemoveGameAsync);
        }
        return mapGroup.WithParameterValidation();
    }

    private static async Task<List<GameSummaryDto>> GetAllGamesAsync(GameStoreContext gameStoreContext)
    {
        return await gameStoreContext.Games
            .Include(g => g.Genre)
            .Select(g => g.ToGameSummaryDto())
            .AsNoTracking()
            .ToListAsync();
    }

    private static async Task<IResult> GetGameAsync(int id, GameStoreContext gameStoreContext)
    {
        Game? game = await gameStoreContext.Games.FindAsync(id);
        return game == null ? Results.NoContent() : Results.Ok(game.ToGameDetailsDto());
    }

    private static async Task<IResult> AddGameAsync(CreateGameDto newGame, GameStoreContext gameStoreContext)
    {
        Game game = newGame.ToEntity();

        await gameStoreContext.Games.AddAsync(game);
        await gameStoreContext.SaveChangesAsync();

        return Results.CreatedAtRoute(
            GetGameRouteName,
            new { id = game.Id },
            game.ToGameDetailsDto()
        );
    }

    private static async Task<IResult> UpdateGameAsync(int id, UpdateGameDto updateGameDto, GameStoreContext gameStoreContext)
    {
        Game? existingGame = await gameStoreContext.Games.FindAsync(id);
        if (existingGame == null) return Results.NotFound();

        Game updateGame = updateGameDto.ToEntity(existingGame.Id);
        gameStoreContext.Entry(existingGame).CurrentValues.SetValues(updateGame);
        await gameStoreContext.SaveChangesAsync();

        return Results.NoContent();
    }

    private static async Task<IResult> RemoveGameAsync(int id, GameStoreContext gameStoreContext)
    {
        var deleteCount = await gameStoreContext.Games
            .Where(g => g.Id == id)
            .ExecuteDeleteAsync();

        if (deleteCount == 0) return Results.NotFound();
        return Results.NoContent();
    }
}