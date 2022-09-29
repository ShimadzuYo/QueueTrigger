using System.Text.Json;
using Function;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using API;
namespace Trigger;

public class QueueTrigger
{
    private readonly ILogger<QueueTrigger> _logger;
    private readonly ProviderApi _providerApi;

    public QueueTrigger(ILogger<QueueTrigger> logger, ProviderApi providerApi)
    {
        _logger = logger;
        _providerApi = providerApi;
    }

    [FunctionName("QueueTrigger")]
    public async Task RunAsync([QueueTrigger("%QueueName%", Connection = "")] string myQueueItem, ILogger log)
    {
        var message = JsonSerializer.Deserialize<Message>(myQueueItem);
        
        log.LogInformation($"C# Queue trigger function processed: {myQueueItem}");
        var job = await _providerApi.GetJob(message.ProviderId, message.JobId);
        Console.WriteLine(job.JobId, job.ProviderId);

    }
}