﻿using Microsoft.Extensions.DependencyInjection;
using PromptGPT.Clients;
using PromptGPT.Services;

namespace PromptGPT.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddPromptGPTServices(this IServiceCollection services)
        {
            services.AddSingleton<HttpClient>();
            services.AddSingleton<AzureOpenAIService>();
            services.AddSingleton<ChatPromptService>();
            services.AddSingleton<ModelDeploymentService>();
            services.AddSingleton<AzureOpenAIClient>();
        }

        public static void AddAppSettings(this IServiceCollection services, string uri, string key)
        {
            var appSettings = new AppSettings(uri, key);

            services.AddSingleton(appSettings);
        }
    }
}
