using System.Net.Http.Headers;
using Function;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SuperProvider.Packages.Logging;

namespace Trigger;

public class Program
{
    public static IHostBuilder CreateHostBuilder(string[] args)
        => new HostBuilder()
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
            });

    public static void Main(string[] args)
    {
        var host = CreateHostBuilder(args).Build();
        host.Run();
    }
}