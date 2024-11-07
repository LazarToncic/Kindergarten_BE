using Kindergarten.Application.Common.Interfaces;
using MediatR;

namespace Kindergarten.Application.Auth.Commands;

public record UserLogoutCommand(string RefreshToken) : IRequest;

public class UserLogoutCommandHandler(IAuthService authService) : IRequestHandler<UserLogoutCommand>
{
    public async Task Handle(UserLogoutCommand request, CancellationToken cancellationToken)
    {
        await authService.UserLogoutAsync(request.RefreshToken);
    }
} 
