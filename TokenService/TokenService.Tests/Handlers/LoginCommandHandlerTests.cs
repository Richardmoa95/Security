using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TokenService.Application.DTOs;
using TokenService.Application.Feature.Auth.Commands;
using TokenService.Application.Feature.Auth.Handlers;
using TokenService.Core.Entities;
using TokenService.Core.Exceptions;
using TokenService.Core.Interfaces;

namespace TokenService.Tests.Handlers;

public class LoginCommandHandlerTests
{
    private readonly Mock<IUserRepository> _userRepoMock = new();
    private readonly Mock<ITokenService> _tokenServiceMock = new();
    private readonly Mock<IPasswordHasher> _hasherMock = new();

    [Fact]
    public async Task Handle_InvalidUser_ThrowsException()
    {
        var handler = new LoginCommandHandler(_userRepoMock.Object, _tokenServiceMock.Object, _hasherMock.Object);

        _userRepoMock.Setup(r => r.GetByEmailAsync("test@example.com")).ReturnsAsync((User)null);

        var command = new LoginCommand(new LoginRequestDto { Email = "test@example.com", Password = "1234" });

        await Assert.ThrowsAsync<UserNotFoundException>(() => handler.Handle(command, default));
    }

    [Fact]
    public async Task Handle_InvalidPassword_ThrowsException()
    {
        var user = new User { Email = "test@example.com", PasswordHash = "hashed" };

        _userRepoMock.Setup(r => r.GetByEmailAsync(user.Email)).ReturnsAsync(user);
        _hasherMock.Setup(h => h.VerifyPassword("hashed", "wrong")).Returns(false);

        var handler = new LoginCommandHandler(_userRepoMock.Object, _tokenServiceMock.Object, _hasherMock.Object);
        var command = new LoginCommand(new LoginRequestDto { Email = user.Email, Password = "wrong" });

        await Assert.ThrowsAsync<UserNotFoundException>(() => handler.Handle(command, default));
    }

    [Fact]
    public async Task Handle_ValidRequest_ReturnsToken()
    {
        var user = new User { Id = Guid.NewGuid(), FullName = "Test User", Email = "test@example.com", PasswordHash = "hashed" };
        var token = "generated.jwt.token";

        _userRepoMock.Setup(r => r.GetByEmailAsync(user.Email)).ReturnsAsync(user);
        _hasherMock.Setup(h => h.VerifyPassword(user.PasswordHash, "1234")).Returns(true);
        _tokenServiceMock.Setup(t => t.GenerateToken(user)).Returns(token);

        var handler = new LoginCommandHandler(_userRepoMock.Object, _tokenServiceMock.Object, _hasherMock.Object);
        var command = new LoginCommand(new LoginRequestDto { Email = user.Email, Password = "1234" });

        var result = await handler.Handle(command, default);

        result.Token.Should().Be(token);
        result.Email.Should().Be(user.Email);
        result.FullName.Should().Be(user.FullName);
    }
}
