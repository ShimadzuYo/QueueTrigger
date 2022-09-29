using API;
using Azure.Identity;
using Azure.Storage.Queues;
using Microsoft.Extensions.Azure;
using QueueTrigger;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var configuration = builder.Configuration;
var services = builder.Services;
services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();
services.AddOptions();
services.AddHostedService<BackGroundService>();


services.Configure<QueueConfiguration>(configuration.GetSection("QueueConfiguration"));
services.AddAzureClients(azureClientFactoryBuilder =>
{
    var queueConfiguration = configuration.GetSection("QueueConfiguration").Get<QueueConfiguration>();
    azureClientFactoryBuilder.UseCredential(new DefaultAzureCredential());
    azureClientFactoryBuilder
        .AddQueueServiceClient(queueConfiguration.StorageConnectionString)
        .ConfigureOptions(c => c.MessageEncoding = QueueMessageEncoding.Base64);
});

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();