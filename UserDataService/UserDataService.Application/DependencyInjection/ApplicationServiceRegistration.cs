using MediatR;
using Microsoft.Extensions.DependencyInjection;
using UserDataService.Application.Features.Users.Queries.GetUserById;

namespace UserDataService.Application.DependencyInjection;

public static class ApplicationServiceRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddMediatR(typeof(GetUserByIdHandler).Assembly);

        return services;
    }
}
