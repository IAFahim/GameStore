using Microsoft.EntityFrameworkCore;

namespace GameStore.API.Data;

public static class DataExtensions
{
    public static void MigrateDb(this WebApplication app)
    {
        using IServiceScope scope = app.Services.CreateScope();
        GameStoreContext gameStoreContext = scope.ServiceProvider.GetRequiredService<GameStoreContext>();
        gameStoreContext.Database.Migrate();
    }
}