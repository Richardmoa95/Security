using FluentAssertions;
using Moq;
using UserDataService.Core.Entities;
using UserDataService.Core.Interfaces;
using UserDataService.Infrastructure.Services;

namespace UserDataService.Tests.Services;

public class UserServiceTests
{
    private readonly Mock<IUserRepository> _userRepoMock;
    private readonly UserService _userService;

    public UserServiceTests()
    {
        _userRepoMock = new Mock<IUserRepository>();
        _userService = new UserService(_userRepoMock.Object);
    }

    [Fact]
    public async Task GetUserByIdAsync_ReturnsUser_WhenExists()
    {
        var userId = Guid.NewGuid();
        var expectedUser = new User
        {
            Id = userId,
            Email = "test@example.com",
            FullName = "Test User",
            IsActive = true
        };

        _userRepoMock.Setup(repo => repo.GetByIdAsync(userId))
            .ReturnsAsync(expectedUser);

        var result = await _userService.GetUserByIdAsync(userId);

        result.Should().NotBeNull();
        result!.Email.Should().Be(expectedUser.Email);
    }

    [Fact]
    public async Task GetUserByIdAsync_ReturnsNull_WhenNotExists()
    {
        var userId = Guid.NewGuid();

        _userRepoMock.Setup(repo => repo.GetByIdAsync(userId))
            .ReturnsAsync((User?)null);

        var result = await _userService.GetUserByIdAsync(userId);

        result.Should().BeNull();
    }
}
