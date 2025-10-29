var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/Hello/world", () => "Hello World!");

app.Run();
