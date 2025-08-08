using FluentValidation;
using TokenService.Application.Feature.Auth.Commands;

namespace TokenService.Application.Feature.Auth.Validators;

public class LoginCommandValidator : AbstractValidator<LoginCommand>
{
    public LoginCommandValidator()
    {
        RuleFor(x => x.Request.Email)
            .NotEmpty().WithMessage("Email requerido.")
            .EmailAddress().WithMessage("Email invalido");

        RuleFor(x => x.Request.Password)
            .NotEmpty().WithMessage("Contraseña requerida.");
    }
}
