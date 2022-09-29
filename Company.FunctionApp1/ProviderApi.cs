using System.Text.Json;
using Trigger;

namespace Company.FunctionApp1;

public class ProviderApi
{
    private readonly HttpClient _httpClient;
    private readonly JsonSerializerOptions _serializerOptions;

    public ProviderApi(HttpClient httpClient, JsonSerializerOptions serializerOptions)
    {
        _httpClient = httpClient;
        _serializerOptions = new JsonSerializerOptions()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true,
        };
    }

    

    public async Task<Job> GetJob(string providerId, string jobId)
    {
        var httpResponseMessage = await _httpClient.GetAsync($"v2/providers{providerId}/jobs/{jobId}");
        var responseContentAsString = await httpResponseMessage.Content.ReadAsStringAsync();
        var job = JsonSerializer.Deserialize<Job>(responseContentAsString);
        return job;
    }
}