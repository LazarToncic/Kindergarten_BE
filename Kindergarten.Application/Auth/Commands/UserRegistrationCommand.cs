using Kindergarten.Application.Common.Dto.Auth;
using Kindergarten.Application.Common.Interfaces;
using MediatR;

namespace Kindergarten.Application.Auth.Commands;

public record UserRegistrationCommand(RegisterDto Dto) : IRequest;

public class UserRegistrationCommandHandler(IAuthService authService) : IRequestHandler<UserRegistrationCommand>
{
    public async Task Handle(UserRegistrationCommand request, CancellationToken cancellationToken)
    {
        await authService.RegisterAsync(request.Dto);
    }
} 