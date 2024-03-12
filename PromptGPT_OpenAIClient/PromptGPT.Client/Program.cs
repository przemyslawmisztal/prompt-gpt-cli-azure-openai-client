using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PromptGPT.Client;
using PromptGPT.Extensions;

var builder = new ConfigurationBuilder()
                 .AddJsonFile($"appsettings.json", true, true);

var config = builder.Build();

var uri = config["AZURE_OPEN_AI:RESOURCE_URI"];
var key = config["AZURE_OPENAI_API_KEY"];
var localStoragePath = Directory.GetCurrentDirectory();

if (string.IsNullOrEmpty(uri))
    uri = args[0];

if (string.IsNullOrEmpty(key))
    key = args[1];

using IHost host = CreateHostBuilder(args, uri, key, localStoragePath).Build();
using var scope = host.Services.CreateScope();

var services = scope.ServiceProvider;

try
{
    services.GetRequiredService<App>().Run(args);
}
catch (Exception e)
{
    Console.WriteLine(e.Message);
}



static IHostBuilder CreateHostBuilder(string[] args, string uri, string key, string localStoragePath)
{
    return Host.CreateDefaultBuilder(args)
        .ConfigureServices((_, services) =>
        {
            services.AddAppSettings(uri, key);
            services.AddPromptGPTServices();
            services.AddServicesWithLocalStorage(localStoragePath);
            services.AddSingleton<App>();
        });
}
