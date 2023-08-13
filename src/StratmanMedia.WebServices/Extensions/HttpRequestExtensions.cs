using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace StratmanMedia.WebServices.Extensions;

public static class HttpRequestExtensions
{
    public static async Task<T?> ParseRequestBodyAsync<T>(this HttpRequest request)
    {
        using var reader = new StreamReader(request.Body);
        var body = await reader.ReadToEndAsync();
        var obj = JsonConvert.DeserializeObject<T>(body);

        return obj;
    }
}