using System.Net.Http.Headers;
using Company.FunctionApp1;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SuperProvider.Packages.Logging;

var host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults()
    .ConfigureAppConfiguration(config => config
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("local.settings.json", true)
        .AddEnvironmentVariables())
    .UseGraylog()
    .ConfigureServices((context, collection) =>
    {
        var token = context.Configuration.GetSection("Token:Bearer").Get<string>();
        collection.AddHttpClient<ProviderApi>(client =>
        {
            client.BaseAddress = new Uri("https://content-provider-api.staging.lionbridge.com");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        });
    })
    .Build();


host.Run();