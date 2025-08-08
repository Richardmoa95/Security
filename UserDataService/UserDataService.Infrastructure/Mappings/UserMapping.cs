using UserDataService.Application.DTOs;
using UserDataService.Core.Entities;

namespace UserDataService.Infrastructure.Mappings;

public static class UserMapping
{
    public static UserDto ToDto(this User user)
    {
        return new UserDto
        {
            Id = user.Id,
            Email = user.Email,
            FullName = user.FullName
        };
    }
}
