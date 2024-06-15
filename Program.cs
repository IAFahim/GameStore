using GameStore.API.Data;
using GameStore.API.EndPoints;

SQLitePCL.Batteries.Init();
var builder = WebApplication.CreateBuilder(args);
{
    string? connString = builder.Configuration.GetConnectionString("GameStore");
    builder.Services.AddSqlite<GameStoreContext>(connString);
}

{
    var app = builder.Build();
    app.MapGameEndPoints();
    app.Run();
}