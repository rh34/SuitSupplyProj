using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SS.UI.MVC.Configuration;

namespace SS.UI.MVC.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddHttpFactories(this IServiceCollection services, IConfiguration configuration)
        {
            var listOfMicros = new List<HttpClientConfiguration>();
            configuration.GetSection("HttpClientFactorySettings").Bind(listOfMicros);
            foreach (var microClient in listOfMicros)
            {
                services.AddHttpClient(microClient.Name, client =>
                    {
                        client.BaseAddress = new Uri(microClient.Url);
                        client.Timeout = TimeSpan.FromSeconds(3);
                    });

            }
        }
    }
}
