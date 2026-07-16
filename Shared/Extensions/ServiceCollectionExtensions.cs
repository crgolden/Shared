namespace Shared.Extensions;

using Microsoft.Extensions.DependencyInjection;
using OpenTelemetry.Instrumentation.AspNetCore;

/// <summary>
/// Extension methods for configuring shared observability options on an <see cref="IServiceCollection"/>.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Configures <see cref="AspNetCoreTraceInstrumentationOptions"/> to exclude <c>/health</c> requests from tracing.
    /// </summary>
    /// <param name="services">The service collection to configure.</param>
    /// <returns>The same <see cref="IServiceCollection"/> instance for chaining.</returns>
    public static IServiceCollection AddObservability(this IServiceCollection services)
    {
        services.Configure<AspNetCoreTraceInstrumentationOptions>(options =>
        {
            options.Filter = context => !context.Request.Path.StartsWithSegments("/health", StringComparison.OrdinalIgnoreCase);
        });
        return services;
    }
}
