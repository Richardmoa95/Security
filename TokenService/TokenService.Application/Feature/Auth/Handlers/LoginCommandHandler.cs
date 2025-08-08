using MediatR;
using TokenService.Application.DTOs;
using TokenService.Application.Feature.Auth.Commands;
using TokenService.Core.Exceptions;
using TokenService.Core.Interfaces;

namespace TokenService.Application.Feature.Auth.Handlers;

public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginResponseDto>
{
    private readonly IUserRepository _userRepository;
    private readonly ITokenService _tokenService;
    private readonly IPasswordHasher _passwordHasher;

    public LoginCommandHandler(IUserRepository userRepository, ITokenService tokenService, IPasswordHasher passwordHasher)
    {
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        _tokenService = tokenService ?? throw new ArgumentNullException(nameof(tokenService));
        _passwordHasher = passwordHasher ?? throw new ArgumentNullException(nameof(passwordHasher));
    }

    public async Task<LoginResponseDto> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByEmailAsync(request.Request.Email);

        if (user == null || !_passwordHasher.VerifyPassword(user.PasswordHash, request.Request.Password))
        {
            throw new UserNotFoundException();
        }

        var token = _tokenService.GenerateToken(user);

        return new LoginResponseDto
        {
            Token = token,
            FullName = user.FullName,
            Email = user.Email
        };  
    }
}
