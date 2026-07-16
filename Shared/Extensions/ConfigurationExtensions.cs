namespace Shared.Extensions;

using Microsoft.Extensions.Configuration;

public static class ConfigurationExtensions
{
    extension(IConfiguration configuration)
    {
        public T GetRequired<T>(string key)
            where T : notnull
        {
            return configuration.GetValue<T?>(key) ?? throw new InvalidOperationException($"Invalid '{key}'.");
        }

#pragma warning disable SA1009
        public string GetIdentitySecrets()
        {
            var serviceBusConnectionString = configuration.GetRequired<string>("ServiceBusConnectionString");
            return serviceBusConnectionString;
        }
#pragma warning restore SA1009
    }
}
