using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserDataService.Core.Entities;

namespace UserDataService.Infrastructure.Data;

public static class AppDbInitializer
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        context.Database.EnsureCreated();

        if (!context.Users.Any())
        {
            context.Users.AddRange(new[]
            {
                new User { Id = Guid.NewGuid(), Email = "admin@mail.com", FullName = "Admin User", IsActive = true },
                new User { Id = Guid.NewGuid(), Email = "user2@example.com", FullName = "Ana García", IsActive = true }
            });
        }

        context.SaveChanges();
    }
}
