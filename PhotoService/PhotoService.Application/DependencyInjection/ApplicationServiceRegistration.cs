using MediatR;
using Microsoft.Extensions.DependencyInjection;
using PhotoService.Application.Features.Photos.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoService.Application.DependencyInjection;

public static class ApplicationServiceRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddMediatR(typeof(GetAllPhotosQueryHandler).Assembly);
        services.AddMediatR(typeof(GetPhotoByUserIdQueryHandler).Assembly);

        return services;
    }
}
