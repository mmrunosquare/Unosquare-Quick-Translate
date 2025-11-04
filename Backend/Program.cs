var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});
 
var app = builder.Build();
 
app.UseCors("AllowAll");

app.MapGet("/Hello/world", () => "Hola mundo!");

app.Run();

