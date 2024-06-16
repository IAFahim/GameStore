using Microsoft.EntityFrameworkCore;

namespace GameStore.API.Data;

public static class DataExtensions
{
    public static async Task MigrateDbAsync(this WebApplication app)
    {
        using IServiceScope scope = app.Services.CreateAsyncScope();
        GameStoreContext gameStoreContext = scope.ServiceProvider.GetRequiredService<GameStoreContext>();
        await gameStoreContext.Database.MigrateAsync();
    }
}