using Microsoft.Extensions.DependencyInjection;
using PhotoService.Core.Interfaces;
using PhotoService.Infrastructure.External;
using PhotoService.Infrastructure;

namespace PhotoService.Infrastructure.DependencyInjection;

public static class InfrastructureServiceRegistration
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddHttpClient<ReqResApiClient>();
        services.AddScoped<IPhotoService, Services.PhotoService>();
        return services;
    }
}
