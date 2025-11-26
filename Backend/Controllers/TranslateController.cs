namespace QuickTranslate.Middleware.Controllers;

using System.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using QuickTranslate.Middleware.Models;

[ApiController]
[Route("api")]
public class TranslateController : ControllerBase
{
    private readonly IDbConnection _connection;
    public TranslateController(IDbConnection connection) { _connection = connection; }

    [HttpPost]
    public async Task<IActionResult> CreateTranslation([FromBody] CreateTranslationRequest request)
    {
        using var httpClient = new HttpClient();
        var formData = new List<KeyValuePair<string, string>>
        {
          new("q", request.TextoOrigen),
          new("source", request.IdiomaOrigen),
          new("target", request.IdiomaDestino)
        };
        var formContent = new FormUrlEncodedContent(formData);
        var response = await httpClient.PostAsync("http://localhost:5000/translate",formContent);
        var translate = await response.Content.ReadFromJsonAsync<TranslateResponse>();
        Console.WriteLine(translate.TranslatedText);
        try
        {
            if (_connection.State != ConnectionState.Open)
                 _connection.Open();
                
            var query = @"
                INSERT INTO Translation (fecha_traduccion, idioma_origen, idioma_destino, texto_origen, texto_destino)
                VALUES (@fecha, @idiomaOrigen, @idiomaDestino, @textoOrigen, @textoDestino);
                SELECT CAST(SCOPE_IDENTITY() as int)";

            using var command = new SqlCommand(query, (SqlConnection)_connection);
            command.Parameters.AddWithValue("@fecha", DateTime.Now.Date);
            command.Parameters.AddWithValue("@idiomaOrigen", request.IdiomaOrigen);
            command.Parameters.AddWithValue("@idiomaDestino", request.IdiomaDestino);
            command.Parameters.AddWithValue("@textoOrigen", request.TextoOrigen);
            command.Parameters.AddWithValue("@textoDestino", translate.TranslatedText);

            var newId = (int)await command.ExecuteScalarAsync();

            return Ok(new
            {
                Id = newId,
                Message = translate.TranslatedText
            });
        }
        catch (Exception ex)
        {
            return BadRequest($"Error al crear traducción: {ex.Message}");
        }
        return Ok(new
        {
            Id = 1,
            Message = "Traducción creada exitosamente"
        });
    }
}