using System.Text.Json;
using API;
using Azure.Storage.Queues;
using Microsoft.Extensions.Options;

namespace QueueTrigger;

public class BackGroundService : BackgroundService
{
    private readonly PeriodicTimer _timer;
    private ILogger<IHostedService> _logger;
    private readonly QueueClient _queueClient;

    private readonly Message _message;

    public BackGroundService(
        QueueServiceClient queueServiceClient,
        ILogger<IHostedService> logger,
        IOptions<QueueConfiguration> queueConfigurationOptions)
    {
        _message = queueConfigurationOptions.Value.MessageSample;
        _queueClient = queueServiceClient.GetQueueClient(queueConfigurationOptions.Value.QueueName);
        _queueClient.CreateIfNotExists();

        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _timer = new PeriodicTimer(TimeSpan.FromSeconds(60));
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Starting background service...");
        while (await _timer.WaitForNextTickAsync(stoppingToken))
        {
            _logger.LogInformation("Putting message into the {queueName}", _queueClient.Name);
            var messageSerialzied = JsonSerializer.Serialize(_message);
            await _queueClient.SendMessageAsync(messageSerialzied, stoppingToken);
            _logger.LogInformation("Successfully put message into the {queueName}", _queueClient.Name);
        }
    }
}