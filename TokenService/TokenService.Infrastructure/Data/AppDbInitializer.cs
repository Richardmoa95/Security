using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TokenService.Core.Entities;
using TokenService.Core.Interfaces;

namespace TokenService.Infrastructure.Data;

public static class AppDbInitializer
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        var passwordHasher = scope.ServiceProvider.GetRequiredService<IPasswordHasher>();

        context.Database.EnsureCreated();

        if (!context.Users.Any())
        {
            context.Users.Add(new User
            {
                Id = Guid.NewGuid(),
                FullName = "Admin User",
                Email = "admin@mail.com",
                PasswordHash = passwordHasher.HashPassword("Admin123"),
            });
        }

        context.SaveChanges();
    }
}
