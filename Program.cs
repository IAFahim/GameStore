using GameStore.API.EndPoints;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build().MapGameEndPoints();
app.Run();


