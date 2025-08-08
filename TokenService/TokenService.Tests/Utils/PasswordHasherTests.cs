using FluentAssertions;
using TokenService.Core.Interfaces;
using TokenService.Infrastructure.Services;

namespace TokenService.Tests.Utils;

public class PasswordHasherTests
{
    private readonly IPasswordHasher _hasher = new PasswordHasher();

    [Fact]
    public void HashPassword_ReturnsDifferentValue()
    {
        var plain = "123456";
        var hashed = _hasher.HashPassword(plain);

        hashed.Should().NotBe(plain);
    }

    [Fact]
    public void VerifyPassword_ReturnsTrue()
    {
        var plain = "123456";
        var hashed = _hasher.HashPassword(plain);

        _hasher.VerifyPassword(hashed, plain).Should().BeTrue();
    }

    [Fact]
    public void VerifyPassword_ReturnsFalse()
    {
        var hashed = _hasher.HashPassword("correct");

        _hasher.VerifyPassword(hashed, "wrong").Should().BeFalse();
    }
}
