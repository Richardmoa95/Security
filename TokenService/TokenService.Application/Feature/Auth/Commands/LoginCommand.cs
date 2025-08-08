using MediatR;
using TokenService.Application.DTOs;

namespace TokenService.Application.Feature.Auth.Commands;

public class LoginCommand : IRequest<LoginResponseDto>
{
    public LoginRequestDto Request { get; set; }

    public LoginCommand(LoginRequestDto request)
    {
        Request = request;
    }
}
