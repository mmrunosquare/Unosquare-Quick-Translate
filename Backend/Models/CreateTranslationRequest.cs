namespace QuickTranslate.Middleware.Models;
public class CreateTranslationRequest
{
    public string IdiomaOrigen { get; set; }
    public string IdiomaDestino { get; set; }
    public string TextoOrigen { get; set; }
}