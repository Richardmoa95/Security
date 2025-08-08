using Microsoft.EntityFrameworkCore;
using UserDataService.Application.DTOs;
using UserDataService.Application.Interfaces;
using UserDataService.Core.Interfaces;
using UserDataService.Infrastructure.Data;
using UserDataService.Infrastructure.Mappings;

namespace UserDataService.Infrastructure.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<UserDto> GetUserByIdAsync(Guid userId)
    {
        var user = await _userRepository.GetByIdAsync(userId);
        return user?.ToDto()!;
    }
}
