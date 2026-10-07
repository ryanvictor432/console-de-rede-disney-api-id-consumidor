using System.Net.Http.Json;
using System.Text.Json.Serialization;

using HttpClient httpClient = new();

try
{
    DisneyResponse? response = await httpClient.GetFromJsonAsync<DisneyResponse>(
        "https://api.disneyapi.dev/character/423");

    if (string.IsNullOrWhiteSpace(response?.Data?.Name))
    {
        Console.Error.WriteLine("Personagem não encontrado na resposta da API.");
        return;
    }

    Console.WriteLine("Nome:");
    Console.WriteLine(response.Data.Name);
    Console.WriteLine("Imagem:");
    Console.WriteLine(response.Data.ImageUrl ?? "Imagem indisponível.");
}
catch (HttpRequestException exception)
{
    Console.Error.WriteLine($"Erro ao consultar a API: {exception.Message}");
}
catch (TaskCanceledException)
{
    Console.Error.WriteLine("A consulta à API demorou demais.");
}

internal sealed record DisneyResponse(
    [property: JsonPropertyName("data")] DisneyCharacter? Data);

internal sealed record DisneyCharacter(
    [property: JsonPropertyName("name")] string? Name,
    [property: JsonPropertyName("imageUrl")] string? ImageUrl);
