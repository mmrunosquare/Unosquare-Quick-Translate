using System.Data;
using Microsoft.Data.SqlClient;

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
builder.Services.AddControllers();
var connectionString = builder.Configuration.GetConnectionString("DefaultConnectionStrings");
Console.WriteLine(connectionString);
builder.Services.AddScoped<IDbConnection>(provider => new SqlConnection(connectionString));
var app = builder.Build();
 
app.UseCors("AllowAll");
app.UseRouting();
app.MapControllers();
app.MapGet("/Hello/world", () => "Hola mundo!");

app.Run();

