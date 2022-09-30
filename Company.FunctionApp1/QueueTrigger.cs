using System;
using System.Text.Json;
using API;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace Company.FunctionApp1;

public class QueueTrigger
{
    private readonly ILogger<QueueTrigger> _logger;
    private readonly ProviderApi _providerApi;

    public QueueTrigger(ILogger<QueueTrigger> logger, ProviderApi providerApi)
    {
        _logger = logger;
        _providerApi = providerApi;
    }

    [Function("QueueTrigger")]
    public async Task RunAsync([QueueTrigger("%QueueName%", Connection = "")] string myQueueItem)
    {
        var message = JsonSerializer.Deserialize<Message>(myQueueItem, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        });
        
        _logger.LogInformation($"C# Queue trigger function processed: {myQueueItem}");
        var job = await _providerApi.GetJob(message.ProviderId, message.JobId);
        _logger.LogInformation(job.JobId, job.ProviderId);

    }
}