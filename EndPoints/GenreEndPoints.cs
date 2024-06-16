using GameStore.API.Data;
using GameStore.API.Dtos;
using GameStore.API.Mapping;
using Microsoft.EntityFrameworkCore;

namespace GameStore.API.EndPoints;

public static class GenreEndPoints
{
    public static RouteGroupBuilder MapGenreEndPoints(this WebApplication app)
    {
        var mapGrouping = app.MapGroup("genre");
        {
            mapGrouping.MapGet("/", GetAllGenre);
        }
        return mapGrouping;
    }

    private static async Task<List<GenreDto>> GetAllGenre(GameStoreContext gameStoreContext)
    {
        return await gameStoreContext.Genres.Select(g => g.ToDto()).ToListAsync();
    }
}