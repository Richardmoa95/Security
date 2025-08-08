using FluentAssertions;
using Microsoft.Extensions.Configuration;
using TokenService.Core.Entities;
using TokenService.Core.Interfaces;

namespace TokenService.Tests.Services;

public class TokenServiceTests
{
    private readonly IConfiguration _config;
    private readonly ITokenService _tokenService;

    public TokenServiceTests()
    {
        var inMemorySettings = new Dictionary<string, string>
        {
            {"Jwt:Key", "supersecurekey123456789012345678901234"},
            {"Jwt:Issuer", "TestIssuer"},
            {"Jwt:Audience", "TestAudience"},
            {"Jwt:ExpirationMinutes", "60"}
        };

        _config = new ConfigurationBuilder()
            .AddInMemoryCollection(inMemorySettings)
            .Build();

        _tokenService = new Infrastructure.Services.TokenService(_config);
    }

    [Fact]
    public void GenerateToken_ReturnsValidToken()
    {
        var user = new User { Id = Guid.NewGuid(), Email = "user@test.com", FullName = "User Test" };

        var token = _tokenService.GenerateToken(user);

        token.Should().NotBeNullOrEmpty();
        token.Split('.').Length.Should().Be(3);
    }
}
